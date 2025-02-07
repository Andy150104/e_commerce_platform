export interface UploadedImage {
  name: string
  size: number
  imagePreview: string
}

export interface ImageItem {
  original: string
  cropped?: string
}
