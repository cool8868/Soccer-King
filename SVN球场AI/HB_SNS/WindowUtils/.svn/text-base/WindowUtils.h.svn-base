/********************************************************************************
 * 文件名：WindowUtils.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef _WINDOWUTILS_H__
#define _WINDOWUTILS_H__

void    LoadIcon(void);                                     // 载入程序需要的图标
BOOL	InitInstance(HINSTANCE, int);						// 实例初始化函数
ATOM	MyRegisterClass(HINSTANCE hInstance);				// 注册函数
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);		// 主回调函数
void	OnIdle(void);										// 空闲时间处理函数	

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
