/******************************************************************************
*file name    : packetbase.h
*owner        :
*description  :
*modified     : 2008-12-24 22:53:45
******************************************************************************/

/*
$Author$
$Source$
$RCSfile$
$Date$
$Log$
*/

#ifndef PACKETBASE_H
#define PACKETBASE_H
#ifdef _WIN32
#pragma once
#endif

#include "utlbuffer.h"
#include "NiRefObject.h"
#include "NiSmartPointer.h"
#include "zlib.h"

#define PACKET_MAGIC_D ('S' << 8 | 'D') //'DS'
#define PACKET_MAGIC_C ('S' << 8 | 'C') //'CS'

NiSmartPointer(PacketBase);

typedef unsigned short uint16;

class PacketBase : public NiRefObject
{
public:
	typedef size_t OPCODE;
	typedef unsigned short MINOR_OP;
	typedef unsigned short MAJOR_OP;

	struct stream_head_t
	{
		uint16		magic;
		uint16 	    len;
		OPCODE 	    op;
		size_t 	    param;
	};

	class ContentHandler : public CUtlBuffer
	{
	public:
		ContentHandler(void const * pBuffer, int size) : CUtlBuffer(pBuffer, size)
		{

		}

		inline void PutSize_t(size_t s)
		{
			CUtlBuffer::Put(&s, sizeof(s));
		}

		inline size_t GetSize_t(void)
		{
			size_t s = 0;

			CUtlBuffer::Get(&s, sizeof(s));

			return s;
		}
	};

	PacketBase(OPCODE code) : m_OP(code), m_Param(0), m_Content(m_buf, sizeof(m_buf))
	{
		m_compress = 0;
	}

	PacketBase(OPCODE code, size_t param) : m_OP(code), m_Param(param), m_Content(m_buf, sizeof(m_buf))
	{
		m_compress = 0;
	}

/*	PacketBase(PacketBase& p)
	{
		m_Connect = p.m_Connect;
		m_OP = p.m_OP;
		m_Param = p.m_Param;

		m_Content.Put(p.m_Content.Base(), p.m_Content.TellPut());
	}
*/
	OPCODE GetOP() { return m_OP;}

	void SetOP(OPCODE op) { m_OP = op; }

	MAJOR_OP GetMajorOP(void) { return (unsigned short)((m_OP >> 16) & 0xffff); }
	MINOR_OP GetMinorOP(void) { return (unsigned short)((m_OP) & 0xffff); }

	size_t GetParam() { return m_Param; }

	ContentHandler& GetContentHandler()
	{
		return m_Content;
	}

	void SetParam(int param) { m_Param = param; }

	static OPCODE MakeOpcode(MAJOR_OP j, MINOR_OP n)
	{
		return (((j & 0xffff) << 16) | (n & 0xffff));
	}

	void SetCompress(int com) { m_compress = com; }

	void StreamPack(CUtlBuffer& buf)
	{
        char com_buf[1024*200] = {};

		int data_len = GetContentHandler().TellPut();

		char *buf_ptr = (char*)GetContentHandler().Base();

		int magic_code = PACKET_MAGIC_D;

		if (m_compress > 0 && data_len >= m_compress)
		{
			int com_len = sizeof(com_buf);

			//int ir = compress((Bytef*)com_buf, (uLongf*)&com_len, (const Bytef*)GetContentHandler().Base(), data_len);

			//if (ir == Z_OK)
			//{
			//	printf("compress data from len %d to %d\n", data_len, com_len);

			//	data_len = com_len;
			//	buf_ptr = com_buf;
			//	magic_code = PACKET_MAGIC_C;
			//}
		}

		stream_head_t head;

		head.magic = magic_code;
		head.len = data_len;
		head.op = GetOP();
		head.param = GetParam();

		buf.Put(&head, sizeof(head));

		if (data_len > 0)
		{
			buf.Put(buf_ptr, data_len);
		}
	}

	void Copy(PacketBase& packet)
	{
		m_OP = packet.m_OP;
		m_Param = packet.m_Param;

		m_Content.SeekPut(CUtlBuffer::SEEK_HEAD, 0);
		m_Content.SeekGet(CUtlBuffer::SEEK_HEAD, 0);

		m_Content.Put(packet.m_Content.Base(), packet.m_Content.TellPut());
	}

private:
	PacketBase(PacketBase& ) : m_Content(m_buf, sizeof(m_buf)) {}

private:
	OPCODE m_OP;

	size_t m_Param;

	ContentHandler m_Content;

	char m_buf[102400];

	int m_compress;
};

#define MAKE_OPCODE(n) PacketBase::MakeOpcode(OP_MAJOR, n)

namespace Packet
{
	typedef CUtlBuffer Serializer;

	class PacketHeader : public NiRefObject
	{
	public:
		typedef size_t OPCODE;
		typedef unsigned short MINOR_OP;
		typedef unsigned short MAJOR_OP;

		static OPCODE MakeOpcode(MAJOR_OP j, MINOR_OP n)
		{
			return (((j & 0xffff) << 16) | (n & 0xffff));
		}

	protected:
		OPCODE m_OpCode;
	};

	NiSmartPointer(PacketHeader);

	class RPacketHdr : public PacketHeader
	{
	public:
		RPacketHdr(Serializer& ser)
		{
			ser.Get(&m_OpCode, sizeof(m_OpCode));
		}

		OPCODE GetOpCode() { return m_OpCode; }
	};

	NiSmartPointer(RPacketHdr);

	class RPacketBase : protected CUtlBuffer
	{
	public:
		size_t GetSize_t(void)
		{
			size_t s = 0;

			BaseClass::Get(&s, sizeof(s));

			return s;
		}

		char GetChar() { return BaseClass::GetChar(); }
		unsigned char GetUnsignedChar() { return BaseClass::GetUnsignedChar(); }
		short GetShort() { return BaseClass::GetShort(); }
		unsigned short GetUnsignedShort() { return BaseClass::GetUnsignedShort(); }
		int GetInt()  { return BaseClass::GetInt(); }
		int GetIntHex() { return BaseClass::GetIntHex(); }
		unsigned int GetUnsignedInt() { return BaseClass::GetUnsignedInt(); }
		float GetFloat() { return BaseClass::GetFloat(); }
		double GetDouble() { return BaseClass::GetDouble(); }
		void GetString(char* pString, int nMaxLen = 0) { BaseClass::GetString(pString, nMaxLen); }
		void Get(void* pMem, int size) { BaseClass::Get(pMem, size); }

	private:
		typedef CUtlBuffer BaseClass;
	};

	class RPacket : public RPacketBase
	{
	public:
		RPacket(RPacketHdrPtr& rph) : m_Header(rph)
		{

		}

	public:
		RPacketHdrPtr& GetHeader(void) { return m_Header; }

	private:
		RPacketHdrPtr m_Header;
	};

	class WPacketBase : protected CUtlBuffer
	{
	public:
		void PutSize_t(size_t s)
		{
			BaseClass::Put(&s, sizeof(s));
		}

		void PutChar(char c) { BaseClass::PutChar(c); }
		void PutUnsignedChar(unsigned char uc) { BaseClass::PutUnsignedChar(uc); }
		void PutShort(short s) { BaseClass::PutShort(s); }
		void PutUnsignedShort(unsigned short us) { BaseClass::PutUnsignedShort(us); }
		void PutInt(int i) { BaseClass::PutInt(i); }
		void PutUnsignedInt(unsigned int u) { BaseClass::PutUnsignedInt(u); }
		void PutFloat(float f) { BaseClass::PutFloat(f); }
		void PutDouble(double d) { BaseClass::PutDouble(d); }
		void PutString(char const * pString) { BaseClass::PutString(pString); }
		void Put(void const * pMem, int size) { BaseClass::Put(pMem, size); }

	private:
		typedef CUtlBuffer BaseClass;
	};

	class WPacketHdr : public PacketHeader
	{
	public:
		WPacketHdr(OPCODE op)
		{
			m_OpCode = op;
		}

	public:
		void Serialzation(Serializer& ser)
		{
			ser.Put(&m_OpCode, sizeof(m_OpCode));
		}
	};

	NiSmartPointer(WPacketHdr);

	class WPacket : public WPacketBase
	{
	public:
		WPacket(WPacketHdrPtr& wph) : m_Header(wph)
		{

		}

	public:
		WPacketHdrPtr& GetHeader(void) { return m_Header; }

	private:
		WPacketHdrPtr m_Header;
	};
}


#endif // PACKETBASE_H
