/********************************************************************************
 * 文件名：WinMain.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include <windows.h>
#include "WindowUtils.h"

#include "../common/common.h"
#include "../common/Time/PrecisionTimer.h"
#include "../FlowFacade/LocalWorkflowAdapter.h"
#include "../ProcessRender/SoccerPitch.h"
#include "../common/DisplayUtility.h"

HINSTANCE	        hInst;
string              szIconName[2];
HWND		        hWnd;
HDC		            hdcBackBuffer;
HBITMAP	            hBitmap;
HBITMAP	            hOldBitmap;

int cxClient;
int cyClient; 

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    hInst = hInstance;

    MyRegisterClass(hInstance);

    if (!InitInstance (hInstance, nCmdShow)) 
    {
        return FALSE;
    }

    singleton_default<PrecisionTimer>::instance().Start();

    MSG lpMsg;

    while(TRUE)
    {
        if(PeekMessage(&lpMsg, NULL, 0, 0, PM_REMOVE))
        {
            if(lpMsg.message == WM_QUIT)
            {
                break;			
            }

            TranslateMessage(&lpMsg);
            DispatchMessage(&lpMsg);	
        }
        else
        {
            if (singleton_default<PrecisionTimer>::instance().ReadyForNextFrame() && 
                lpMsg.message != WM_QUIT && !DisPlayerController.Pause)
            {
#ifdef REALTIME_MODE
                singleton_default<LocalWorkflowAdapter>::instance().MainLoopForDebug();

#elif defined PARSE_PROCESS_MODE
                singleton_default<SoccerPitch>::instance().Update();

#elif defined PARSE_RESULT_FILE_MODE
                singleton_default<SoccerPitch>::instance().Update();

#elif defined OUTPUT_AND_PARSE_RESULT_FILE_MODE
                singleton_default<SoccerPitch>::instance().Update();

#elif defined TRANSLATE_OUTPUT_FILE_MODE
                singleton_default<SoccerPitch>::instance().Update();

#elif defined PARSE_BUFFER_MODE
                singleton_default<SoccerPitch>::instance().Update();

#elif defined PARSE_INPUT_FILE_MODE
                singleton_default<LocalWorkflowAdapter>::instance().MainLoopForDebug();

#else 
                // None
#endif
            }

            RedrawWindow(hWnd, true);
        }
    }

    return lpMsg.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch(message)
    {
    case WM_CREATE:
        On_Create();
        break;	

    case WM_COMMAND:
        On_Command(wParam);
        break;

    case WM_PAINT:
        On_Paint();
        break;

    case WM_SIZE:
        On_Size(lParam);
        break;

    case WM_DESTROY:
        On_Destroy();
        break;

    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }

    return (0L);
}
