/********************************************************************************
 * �ļ�����WindowUtils.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef _WINDOWUTILS_H__
#define _WINDOWUTILS_H__

void    LoadIcon(void);                                     // ���������Ҫ��ͼ��
BOOL	InitInstance(HINSTANCE, int);						// ʵ����ʼ������
ATOM	MyRegisterClass(HINSTANCE hInstance);				// ע�ắ��
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);		// ���ص�����
void	OnIdle(void);										// ����ʱ�䴦����	

void On_Create(void);
void On_Paint(void);
void On_Destroy(void);
void On_Command(WPARAM wParam);
void On_Size(LPARAM lParam);

void ReCalcSize(int& width, int& height);

void OnCheckMenuItem(HMENU hMenu, int command, bool show);
void ChangeMenuState(HWND hwnd, UINT MenuItem, UINT state);
void OnCheckMenuItemsAll();
void OnRestartMatch();

void RedrawWindow(HWND hwnd, bool RedrawBackGround = true);

#endif  //_WINDOWUTILS_H__
