//========= Copyright ?1996-2001, Valve LLC, All rights reserved. ============
//
// The copyright to the contents herein is the property of Valve, L.L.C.
// The contents may be used and/or copied only with the written permission of
// Valve, L.L.C., or in accordance with the terms and conditions stipulated in
// the agreement/contract under which the contents have been supplied.
//
// $Header: /cvs/myengine/src_main/public/valve/utlbuffer.cpp,v 1.1 2008/02/29 00:05:46 easap\albertyang Exp $
// $NoKeywords: $
//
// Serialization buffer
//=============================================================================

#ifdef _WIN32
#pragma warning (disable : 4514)
#pragma warning (disable : 4311)
#pragma warning (disable : 4267)
#endif


#include "utlbuffer.h"
#include <stdio.h>
#include <stdarg.h>
#include <ctype.h>
#include <stdlib.h>
#include <limits.h>

//-----------------------------------------------------------------------------
// constructors
//-----------------------------------------------------------------------------
CUtlBuffer::CUtlBuffer(int growSize, int initSize, bool text) : m_Memory(growSize, initSize),
	m_Error(0)
{
	m_Get = 0;
	m_Put = 0;
	m_Flags = 0;
	if (text)
	{
		m_Flags |= TEXT_BUFFER;
	}
}

CUtlBuffer::CUtlBuffer(void const * pBuffer, int size, bool text) : m_Memory((unsigned char*) pBuffer, size),
	m_Error(0)
{
	m_Get = 0;
	m_Put = 0;
	m_Flags = 0;
	if (text)
		m_Flags |= TEXT_BUFFER;
}


//-----------------------------------------------------------------------------
// Attaches the buffer to external memory....
//-----------------------------------------------------------------------------
void CUtlBuffer::SetExternalBuffer(void* pMemory, int numElements, bool text)
{
	m_Memory.SetExternalBuffer((unsigned char *) pMemory, numElements);

	// Reset all indices; we just changed memory
	m_Get = 0;
	m_Put = 0;
	m_Flags = 0;
	if (text)
		m_Flags |= TEXT_BUFFER;
}


//-----------------------------------------------------------------------------
// Makes sure we've got at least this much memory
//-----------------------------------------------------------------------------
void CUtlBuffer::EnsureCapacity(int num)
{
	m_Memory.EnsureCapacity(num);
}


//-----------------------------------------------------------------------------
// Base get method from which all others derive
//-----------------------------------------------------------------------------
void CUtlBuffer::Get(void* pMem, int size)
{
	assert(m_Get + size <= m_Memory.NumAllocated());
	memcpy(pMem, &m_Memory[m_Get], size);
	m_Get += size;
}

int CUtlBuffer::GetUTF(char* pString, int nMaxLen)
{
	if (!CheckGet(sizeof(unsigned short)))
	{
		return -1;
	}

	int len = GetUnsignedShort();

	if (len <= nMaxLen && CheckGet(len))
	{
		Get(pString, len);
		pString[len] = 0;
	}
	else
	{
		//Warning("%s, Could not GetUTF string with len[%d], maxlen[%d], mem allocated[%d]\n", __FILE__, len, nMaxLen, m_Memory.NumAllocated());
		return -1;
	}

	return len;
}

//-----------------------------------------------------------------------------
// Eats whitespace
//-----------------------------------------------------------------------------
void CUtlBuffer::EatWhiteSpace()
{
	if (IsText() && IsValid())
	{
		int lastpos = Size();
		while (m_Get < lastpos)
		{
			if (!isspace(*(char *) &m_Memory[m_Get]))
				break;
			m_Get += sizeof(char);
		}
	}
}


//-----------------------------------------------------------------------------
// Reads a null-terminated string
//-----------------------------------------------------------------------------
void CUtlBuffer::GetString(char* pString, int nMaxLen)
{
	if (!IsValid())
	{
		*pString = 0;
		return;
	}

	if (nMaxLen == 0)
	{
		nMaxLen = INT_MAX;
	}

	if (!IsText())
	{
		int len = strlen((char*) &m_Memory[m_Get]) + 1;
		if (len <= nMaxLen)
		{
			Get(pString, len);
		}
		else
		{
			Get(pString, nMaxLen);
			pString[nMaxLen - 1] = 0;
			SeekGet(SEEK_CURRENT, len - nMaxLen);
		}
	}
	else
	{
		// eat all whitespace
		EatWhiteSpace();

		// Eat characters
		int nCount = 0;
		int nLastPos = Size();
		while (m_Get < nLastPos)
		{
			char c = *(char*) &m_Memory[m_Get];
			if (isspace(c) || (!c))
				break;

			if (nCount < nMaxLen - 1)
			{
				*pString++ = c;
			}
			++nCount;
			++m_Get;
		}

		// Terminate
		*pString = 0;
	}
}


//-----------------------------------------------------------------------------
// Checks if a get is ok
//-----------------------------------------------------------------------------
bool CUtlBuffer::CheckGet(int size)
{
	if (m_Error)
		return false;

	if (m_Memory.NumAllocated() >= m_Get + size)
		return true;

	if (m_Put < m_Get + size)
	{
		//Warning("%s, %d, CUtlBuffer get overflow \n", __FUNCTION__, __LINE__);
	}

	m_Error |= GET_OVERFLOW;
	return false;
}


//-----------------------------------------------------------------------------
// Change where I'm reading
//-----------------------------------------------------------------------------
void CUtlBuffer::SeekGet(SeekType_t type, int offset)
{
	switch (type)
	{
	case SEEK_HEAD:
		m_Get = offset;
		break;

	case SEEK_CURRENT:
		m_Get += offset;
		break;

	case SEEK_TAIL:
		m_Get = m_Memory.NumAllocated() - offset;
		break;
	}
}


//-----------------------------------------------------------------------------
// Parse...
//-----------------------------------------------------------------------------
#ifdef _WIN32
#pragma warning ( disable : 4706 )
#endif
int CUtlBuffer::VaScanf(char const * pFmt, va_list list)
{
	assert(pFmt);
	if (m_Error || !IsText())
		return 0;

	int numScanned = 0;

	char c;
	char* pEnd;
	while (!!(c = *pFmt++))
	{
		// Stop if we hit the end of the buffer
		if (m_Get >= Size())
		{
			m_Error |= GET_OVERFLOW;
			break;
		}

		switch (c)
		{
		case ' ':
			// eat all whitespace
			EatWhiteSpace();
			break;

		case '%':
			{
				// Conversion character... try to convert baby!
				char type = *pFmt++;
				if (type == 0)
					return numScanned;

				switch (type)
				{
				case 'c':
					{
						char* ch = va_arg(list, char*);
						*ch = (char) m_Memory[m_Get];
						++m_Get;
					}
					break;

				case 'i':
				case 'd':
					{
						int* i = va_arg(list, int*);
						*i = strtol((char *) PeekGet(), &pEnd, 10);
						if (pEnd == PeekGet())
							return numScanned;
						m_Get = (int) pEnd - (int) Base();
					}
					break;

				case 'x':
					{
						int* i = va_arg(list, int*);
						*i = strtol((char *) PeekGet(), &pEnd, 16);
						if (pEnd == PeekGet())
							return numScanned;
						m_Get = (int) pEnd - (int) Base();
					}
					break;

				case 'u':
					{
						unsigned int * u = va_arg(list, unsigned int *);
						*u = strtoul((char *) PeekGet(), &pEnd, 10);
						if (pEnd == PeekGet())
							return numScanned;
						m_Get = (int) pEnd - (int) Base();
					}
					break;

				case 'f':
					{
						float* f = va_arg(list, float*);
						*f = (float) strtod((char *) PeekGet(), &pEnd);
						if (pEnd == PeekGet())
							return numScanned;
						m_Get = (int) pEnd - (int) Base();
					}
					break;

				case 's':
					{
						char* s = va_arg(list, char*);
						GetString(s);
					}
					break;

				default:
					{
						// unimplemented scanf type
						assert(0);
						return numScanned;
					}
					break;
				}

				++numScanned;
			}
			break;

		default:
			{
				// Here we have to match the format string character
				// against what's in the buffer or we're done.
				if (c != m_Memory[m_Get])
					return numScanned;
				++m_Get;
			}
		}
	}
	return numScanned;
}
#ifdef _WIN32
#pragma warning ( default : 4706 )
#endif

int CUtlBuffer::Scanf(char const * pFmt, ...)
{
	va_list args;

	va_start(args, pFmt);
	int count = VaScanf(pFmt, args);
	va_end(args);

	return count;
}


//-----------------------------------------------------------------------------
// Serialization
//-----------------------------------------------------------------------------

void CUtlBuffer::Put(void const * pMem, int size)
{
	if (CheckPut(size))
	{
		memcpy(&m_Memory[m_Put], pMem, size);
		m_Put += size;
	}
}


//-----------------------------------------------------------------------------
// Writes a null-terminated string
//-----------------------------------------------------------------------------

void CUtlBuffer::PutString(char const * pString)
{
	int len = strlen(pString);

	// Not text? append a null at the end.
	if (!IsText())
		++len;

	Put(pString, len);
}

void CUtlBuffer::PutUTF(char const *pString)
{
	int len = strlen(pString);
	PutShort(len);
	Put(pString, len);
}

void CUtlBuffer::VaPrintf(char const * pFmt, va_list list)
{
	char temp[2048];
	int len = vsprintf(temp, pFmt, list);
	assert(len < 2048);

	// Not text? append a null at the end.
	if (!IsText())
		++len;

	Put(temp, len);
}

void CUtlBuffer::Printf(char const * pFmt, ...)
{
	va_list args;

	va_start(args, pFmt);
	VaPrintf(pFmt, args);
	va_end(args);
}


//-----------------------------------------------------------------------------
// Checks if a put is ok
//-----------------------------------------------------------------------------

bool CUtlBuffer::CheckPut(int size)
{
	if (m_Error)
		return false;

	while (m_Memory.NumAllocated() < m_Put + size)
	{
		if (m_Memory.IsExternallyAllocated())
		{
			m_Error |= PUT_OVERFLOW;
			return false;
		}

		m_Memory.Grow();
	}
	return true;
}

void CUtlBuffer::SeekPut(SeekType_t type, int offset)
{
	switch (type)
	{
	case SEEK_HEAD:
		m_Put = offset;
		break;

	case SEEK_CURRENT:
		m_Put += offset;
		break;

	case SEEK_TAIL:
		m_Put = m_Memory.NumAllocated() - offset;
		break;
	}
}
