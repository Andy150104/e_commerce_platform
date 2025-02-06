import type { UploadedImage } from '@PKG_SRC/types/models/imageTypes'
import { defineStore } from 'pinia'

export type UploadImageState = {
  uploadImage: UploadedImage[]
}
export const useUploadImageStore = defineStore('Upload Image', {
  state: (): UploadImageState => ({
    uploadImage: [],
  }),
  getters: {},
  actions: {
    ResetStore() {
      this.uploadImage = []
    },
    SetUploadImage(name: string, size: number, imagePreview: string) {
      this.uploadImage.push({
        name: name,
        size: size,
        imagePreview: imagePreview,
      })
    },
  },
})
