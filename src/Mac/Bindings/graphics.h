#ifndef __GRAPHICS_H__
#define __GRAPHICS_H__

extern "C" bool InitializeViewport(int width, int height, bool fullscreen);

extern "C" void DrawTexture2D(unsigned int textureId, float xPos, float yPos, float zPos, float rotation,
	float texLeft, float texTop, float texRight, float texBottom, float width, float height);

extern "C" void CleanBackBuffer();

extern "C" void SwapBackBuffer();

extern "C" bool GetIsWindowOpened();

extern "C" void ReleaseViewport();

#endif