import type { UploadedImage } from '@PKG_SRC/types/models/imageTypes'
import axios from 'axios'
import { defineStore } from 'pinia'

export type UploadImageState = {
  uploadImage: UploadedImage[]
  image: string
}
export const useUploadImageStore = defineStore('Upload Image', {
  state: (): UploadImageState => ({
    uploadImage: [],
    image: ''
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
    SetImage(imageList: string[]) {
      this.ResetStore()
      imageList.forEach((image) => {
        this.uploadImage.push({
          name: 'image',
          size: 1024,
          imagePreview: image,
        })
      })
    },
    async uploadCloudinaryBase64(base64: string): Promise<void> {
      const cloudName = 'dbfokyruf'
      const uploadPreset = 'EcomBox'
      const url = `https://api.cloudinary.com/v1_1/${cloudName}/upload`
      const formData = new FormData()
      formData.append('file', base64)
      formData.append('upload_preset', uploadPreset)
      try {
        const response = await axios.post(url, formData)
        this.image = response.data.secure_url
        return response.data.secure_url
      } catch (error) {
        console.error('Error uploading base64 image to Cloudinary:', error)
        throw error
      }
    },
  },
})
