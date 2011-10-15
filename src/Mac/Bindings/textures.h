#ifndef __TEXTURES_H__
#define __TEXTURES_H__

struct TextureInfo
{
	unsigned int textureId;
	unsigned int width;
	unsigned int height;
};

extern "C" TextureInfo LoadTexture2DFromMemory(void *data, unsigned int size);

extern "C" TextureInfo LoadTexture2DFromFile(const char* path);

extern "C" void UnloadTexture(unsigned int textureId);

#endif