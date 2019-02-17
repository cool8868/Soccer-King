/********************************************************************************
 * 文件名：WindowUtils.cpp
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
#include "DefineUtils.h"

#include "../common/misc/Cgdi.h"
#include "../common/Time/PrecisionTimer.h"
#include "../FlowFacade/LocalWorkflowAdapter.h"
#include "../ProcessRender/SoccerPitch.h"
#include "../ProcessOutput/ResultEntity.h"
#include "../common/Defines/Defines.h"
#include "../common/DisplayUtility.h"
#include "../ProcessParser/ParseEntity.h"

using boost::details::pool::singleton_default;

extern HWND			hWnd;
extern HDC          hdcBackBuffer;
extern HINSTANCE	hInst;
extern string       szIconName[2];

extern int          cxClient;
extern int          cyClient; 

extern HBITMAP      hBitmap;
extern HBITMAP	    hOldBitmap;

char* g_szApplicationName   = "热血球球SNS";
char* g_szClassName         = "HB_SNS";
char* g_szMenuName          = "热血球球SNS";

void LoadIcon(void)                                // 载入图标信息资源
{
    for(int i = 0; i < 2; i++)                          
    {
        format fmt("图标%d");
        fmt % i;

        szIconName[i] = fmt.str();
    }
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEX wcApp;

    LoadIcon();                                     // 载入图标信息资源

    wcApp.lpszClassName = g_szClassName;
    wcApp.hInstance		= hInstance;
    wcApp.lpfnWndProc	= WndProc;
    wcApp.hCursor		= LoadCursor(NULL, IDC_ARROW);
    wcApp.hIcon			= LoadIcon(hInst, szIconName[0].c_str());
    wcApp.hIconSm       = LoadIcon(hInst, szIconName[1].c_str());
    wcApp.lpszMenuName	= g_szMenuName;
    wcApp.hbrBackground	= (HBRUSH)NULL;
    wcApp.style			= CS_HREDRAW | CS_VREDRAW;
    wcApp.cbClsExtra	= 0;
    wcApp.cbWndExtra	= 0;
    wcApp.cbSize        = sizeof(WNDCLASSEX);

    return RegisterClassEx(&wcApp);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
    int cxSource, cySource;

    int width   = static_cast<int>(Defines_Pitch.MAX_WIDTH * SCALE_SIZE + 2 * PITCH_BOUNDS);
    int height  = static_cast<int>(Defines_Pitch.MAX_HEIGHT * SCALE_SIZE + 2 * PITCH_BOUNDS);

    cxSource = 2 * GetSystemMetrics (SM_CXSIZEFRAME) + width;

    // 边框和标题栏的高度
    cySource = 2 * GetSystemMetrics (SM_CYSIZEFRAME) + 
               GetSystemMetrics (SM_CYCAPTION) + GetSystemMetrics (SM_CYMENUSIZE) + height;

    hWnd = CreateWindow (g_szClassName, g_szApplicationName, 
        WS_OVERLAPPEDWINDOW,
        120, 20, cxSource, cySource,
        NULL,
        NULL,
        hInstance,
        NULL);

    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);

    return TRUE;
}

void On_Create(void)
{
    RECT rect;

    GetClientRect(hWnd, &rect);

    cxClient = rect.right;
    cyClient = rect.bottom;

    hdcBackBuffer = CreateCompatibleDC(NULL);

    HDC hdc = GetDC(hWnd);

    hBitmap = CreateCompatibleBitmap(hdc,
        cxClient,
        cyClient);

    hOldBitmap = (HBITMAP)SelectObject(hdcBackBuffer, hBitmap);

    ReleaseDC(hWnd, hdc); 

    OnCheckMenuItemsAll();

#ifdef REALTIME_MODE
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<LocalWorkflowAdapter>::instance().CreateMatchForDebug(0, 1, 0, 2));

#elif defined PARSE_PROCESS_MODE
    singleton_default<LocalWorkflowAdapter>::instance().CreateMatch(0, 1, 0, 2);
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<LocalWorkflowAdapter>::instance().GetMatch());

#elif defined PARSE_RESULT_FILE_MODE
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<ParseEntity>::instance().Attach("Result.bin"));

#elif defined OUTPUT_AND_PARSE_RESULT_FILE_MODE
    singleton_default<LocalWorkflowAdapter>::instance().CreateMatch(0, 1, 0, 2);
    singleton_default<ResultEntity>::instance().Attach(singleton_default<LocalWorkflowAdapter>::instance().GetMatch());
    singleton_default<ResultEntity>::instance().Output("Result.bin");
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<ParseEntity>::instance().Attach("Result.bin"));

#elif defined TRANSLATE_OUTPUT_FILE_MODE
    singleton_default<LocalWorkflowAdapter>::instance().CreateMatch(0, 1, 0, 2);
    singleton_default<ResultEntity>::instance().Attach(singleton_default<LocalWorkflowAdapter>::instance().GetMatch());
    singleton_default<ResultEntity>::instance().Output("Result.bin");
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<ParseEntity>::instance().Attach("Result.bin"));
    singleton_default<ParseEntity>::instance().TranslateOutput("UserFile.txt");

#elif defined PARSE_BUFFER_MODE
    singleton_default<LocalWorkflowAdapter>::instance().CreateMatch(0, 1, 0, 2);
    singleton_default<ResultEntity>::instance().Attach(singleton_default<LocalWorkflowAdapter>::instance().GetMatch());
    singleton_default<ResultEntity>::instance().OutputBuff();
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<ParseEntity>::instance().Attach(singleton_default<ResultEntity>::instance().GetMatchPacket()));

#elif defined PARSE_INPUT_FILE_MODE
    singleton_default<SoccerPitch>::instance().Attach(singleton_default<LocalWorkflowAdapter>::instance().CreateMatchFromFile("EntityData.bin"));

#else
    //None

#endif

    for (int i = DisPlayCollection::PLAY_MIN; i < DisPlayCollection::PLAY_MAX; ++i)
    {
        if (DisPlayerController.PlaySpeed[i].enable)
        {
            singleton_default<PrecisionTimer>::instance().SetFps(DisPlayerController.PlaySpeed[i].fps);
        }        
    }    
}

void On_Command(WPARAM wParam)
{
    int command = LOWORD(wParam);

    switch (command)
    {
    case IDM_PLAY_SLOW:
        {
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_SLOW].enable      = true;
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_NORMAL].enable    = false;
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_FAST].enable      = false;

            singleton_default<PrecisionTimer>::instance().SetFps(DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_SLOW].fps);
        }
        break;

    case IDM_PLAY_NORMAL:
        {
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_SLOW].enable      = false;
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_NORMAL].enable    = true;
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_FAST].enable      = false;

            singleton_default<PrecisionTimer>::instance().SetFps(DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_NORMAL].fps);
        }
        break;

    case IDM_PLAY_FAST:
        {
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_SLOW].enable      = false;
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_NORMAL].enable    = false;
            DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_FAST].enable      = true;

            singleton_default<PrecisionTimer>::instance().SetFps(DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_FAST].fps);
        }
        break;

    case IDM_MATCH_PAUSE:
        {
            DisPlayerController.Pause = !DisPlayerController.Pause;
        }
        break;

    case IDM_MATCH_RESTART:
        {
            singleton_default<SoccerPitch>::instance().Restart();
        }
        break;

    case IDM_PLAYER_ID:
        {
            DisPlayerController.PlayerId = !DisPlayerController.PlayerId;
        }
        break;

    case IDM_PLAYER_STATE:
        {
            DisPlayerController.PlayerState = !DisPlayerController.PlayerState;
        }
        break;

    case IDM_PLAYER_DIRECTION:
        {
            DisPlayerController.PlayerDirection = !DisPlayerController.PlayerDirection;
        }
        break;

    case IDM_PLAYER_DEFENCERANGE:
        {
            DisPlayerController.PlayerDefenceRange  = !DisPlayerController.PlayerDefenceRange;
        }
        break;

    case IDM_PLAYER_INTERRUPTIONRANGE:
        {
            DisPlayerController.PlayerInterRuptionRange = !DisPlayerController.PlayerInterRuptionRange;
        }
        break;


    case IDM_PLAYER_SIGHTRANGE:
        {
            DisPlayerController.PlayerSightRange    = !DisPlayerController.PlayerSightRange;
        }
        break;

    case IDM_FOOTBALL_POINT:
        {
            DisPlayerController.FootBallPoint = !DisPlayerController.FootBallPoint;
        }
        break;

    default:
        break;
    }

    //更新菜单状态
    OnCheckMenuItemsAll();
}

void On_Size(LPARAM lParam)
{
    cxClient = LOWORD(lParam);
    cyClient = HIWORD(lParam);

    ReCalcSize(cxClient, cyClient);

    SelectObject(hdcBackBuffer, hOldBitmap);

    DeleteObject(hBitmap); 

    HDC hdc = GetDC(hWnd);

    hBitmap = CreateCompatibleBitmap(hdc,
        cxClient,
        cyClient);

    ReleaseDC(hWnd, hdc);

    SelectObject(hdcBackBuffer, hBitmap);
}

void On_Paint(void)
{
    PAINTSTRUCT ps;

    BeginPaint (hWnd, &ps);

    gdi->StartDrawing(hdcBackBuffer);

#ifdef REALTIME_MODE
    singleton_default<SoccerPitch>::instance().RenderByPlayer();

#elif defined PARSE_PROCESS_MODE
    singleton_default<SoccerPitch>::instance().RenderByProcess();

#elif defined PARSE_RESULT_FILE_MODE
    singleton_default<SoccerPitch>::instance().RenderByParserProcess();

#elif defined OUTPUT_AND_PARSE_RESULT_FILE_MODE
    singleton_default<SoccerPitch>::instance().RenderByParserProcess();

#elif defined TRANSLATE_OUTPUT_FILE_MODE
    singleton_default<SoccerPitch>::instance().RenderByParserProcess();

#elif defined PARSE_BUFFER_MODE
    singleton_default<SoccerPitch>::instance().RenderByParserProcess();

#elif defined PARSE_INPUT_FILE_MODE
    singleton_default<SoccerPitch>::instance().RenderByPlayer();

#else
    // None

#endif

    gdi->StopDrawing(hdcBackBuffer);

    BitBlt(ps.hdc, 0, 0, cxClient, cyClient, hdcBackBuffer, 0, 0, SRCCOPY);

    EndPaint(hWnd, &ps);
}

void On_Destroy(void)
{
    SelectObject(hdcBackBuffer, hOldBitmap);

    DeleteDC(hdcBackBuffer);
    DeleteObject(hBitmap); 

    PostQuitMessage(0);
}

void RedrawWindow(HWND hwnd, bool RedrawBackGround)
{
    InvalidateRect(hwnd, NULL, RedrawBackGround);

    UpdateWindow(hwnd);
}

void OnCheckMenuItem(HWND hwnd, int command, bool show)
{
    if (show)
    {
        ChangeMenuState(hwnd, command, MF_CHECKED);
    }
    else
    {
        ChangeMenuState(hwnd, command, MF_UNCHECKED);
    }
}

void ChangeMenuState(HWND hwnd, UINT MenuItem, UINT state)
{
    MENUITEMINFO mi;

    mi.cbSize = sizeof(MENUITEMINFO);
    mi.fMask  = MIIM_STATE;
    mi.fState = state;

    SetMenuItemInfo(GetMenu(hwnd), MenuItem, false, &mi);
    DrawMenuBar(hwnd);
}

void OnCheckMenuItemsAll()
{
    OnCheckMenuItem(hWnd, IDM_PLAY_SLOW,                    DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_SLOW].enable);
    OnCheckMenuItem(hWnd, IDM_PLAY_NORMAL,                  DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_NORMAL].enable);
    OnCheckMenuItem(hWnd, IDM_PLAY_FAST,                    DisPlayerController.PlaySpeed[DisPlayCollection::PLAY_FAST].enable);

    OnCheckMenuItem(hWnd, IDM_MATCH_PAUSE,                  DisPlayerController.Pause);

    OnCheckMenuItem(hWnd, IDM_PLAYER_ID,                    DisPlayerController.PlayerId);
    OnCheckMenuItem(hWnd, IDM_PLAYER_STATE,                 DisPlayerController.PlayerState);
    OnCheckMenuItem(hWnd, IDM_PLAYER_DIRECTION,             DisPlayerController.PlayerDirection);
    OnCheckMenuItem(hWnd, IDM_PLAYER_DEFENCERANGE,          DisPlayerController.PlayerDefenceRange);
    OnCheckMenuItem(hWnd, IDM_PLAYER_INTERRUPTIONRANGE,     DisPlayerController.PlayerInterRuptionRange);
    OnCheckMenuItem(hWnd, IDM_PLAYER_SIGHTRANGE,            DisPlayerController.PlayerSightRange);

    OnCheckMenuItem(hWnd, IDM_FOOTBALL_POINT,               DisPlayerController.FootBallPoint);
}

void OnRestartMatch()
{
    singleton_default<SoccerPitch>::instance().Restart();
}

void ReCalcSize(int& width, int& height)
{
    int sysCx = GetSystemMetrics (SM_CXSIZEFRAME);
    int sysCy = 2 * GetSystemMetrics (SM_CYSIZEFRAME) + 
        GetSystemMetrics (SM_CYCAPTION) + GetSystemMetrics (SM_CYMENUSIZE);

    double Ori_Rate = ((double)Defines_Pitch.MAX_WIDTH) / (Defines_Pitch.MAX_HEIGHT);
    double Cur_Rate = (double)(width + sysCx - 2 * PITCH_BOUNDS)/ (height + sysCy - 2 * PITCH_BOUNDS);

    // 宽比较小
    if (Cur_Rate < Ori_Rate)
    {
        height = static_cast<int>(width / Ori_Rate) + sysCy;
    }
    else
    {
        width = static_cast<int>(height * Ori_Rate) + sysCx;
    }

    double  size = ((double)width + 2 * sysCx - 2 * PITCH_BOUNDS) / ((float)Defines_Pitch.MAX_WIDTH);

    //确定缩放比例
    DisPlayerController.ScaleSize = size;

    //应用缩放比例
    singleton_default<SoccerPitch>::instance().ReSize();
}
