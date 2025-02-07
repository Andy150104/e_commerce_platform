<template>
  <div class="max-w-6xl mx-auto p-5">
    <!-- Input chọn file ảnh (cho phép chọn nhiều ảnh) -->
    <input
      type="file"
      @change="onFileChange"
      accept="image/*"
      multiple
      class="mb-5 block"
    />

    <!-- Nếu có ảnh, hiển thị giao diện crop & preview -->
    <div v-if="images.length" class="space-y-5">
      <!-- Container chứa cropper và preview của ảnh đã crop (xếp thành cột trên mobile, hàng ngang trên md) -->
      <div class="flex flex-col md:flex-row gap-5">
        <!-- Phần cropper -->
        <div class="flex-1">
          <!-- Thanh công cụ điều khiển -->
          <div class="flex flex-wrap gap-2 mb-3">
            <button
              @click="setSquare"
              class="px-3 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              Hình vuông
            </button>
            <button
              @click="setRectangle"
              class="px-3 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              Hình chữ nhật
            </button>
            <button
              @click="rotateLeft"
              class="px-3 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              Xoay trái
            </button>
            <button
              @click="rotateRight"
              class="px-3 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              Xoay phải
            </button>
            <!-- Nút Reset: reset vị trí ảnh và khung cắt -->
            <button
              @click="resetCropper"
              class="px-3 py-2 bg-red-500 text-white rounded hover:bg-red-600"
            >
              Reset
            </button>
          </div>

          <!-- Cropper (sử dụng key theo currentIndex để khởi tạo lại mỗi khi chuyển ảnh) -->
          <VueCropper
            ref="cropper"
            :src="currentImage.original"
            :aspect-ratio="aspectRatio"
            :auto-crop-area="1"
            :view-mode="1"
            drag-mode="move"
            preview=".preview"
            class="w-full h-[26rem] bg-gray-100"
            :key="currentIndex"
          />

          <!-- Nút Crop Image -->
          <button
            @click="cropImage"
            class="mt-3 px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600"
          >
            Crop Image
          </button>
        </div>

        <!-- Phần preview của ảnh đã crop -->
        <div class="flex-1 flex flex-col items-center justify-center border border-gray-300 p-2 shadow-lg">
          <h3 class="text-lg font-semibold mb-2">
            Ảnh đã cắt ({{ currentIndex + 1 }} / {{ images.length }})
          </h3>
          <div v-if="currentImage.cropped">
            <img :src="currentImage.cropped" alt="Cropped Image" class="w-full" />
          </div>
          <div v-else class="text-gray-500">
            Chưa có ảnh đã cắt.
          </div>
        </div>
      </div>

      <!-- Navigation: Back & Next -->
      <div class="flex justify-between mt-5">
        <button
          @click="previousImage"
          :disabled="currentIndex === 0"
          class="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          Back
        </button>
        <button
          @click="nextImage"
          :disabled="currentIndex === images.length - 1"
          class="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          Next
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import VueCropper from 'vue-cropperjs'
import 'cropperjs/dist/cropper.css'

// Định nghĩa kiểu cho từng ảnh
interface ImageItem {
  original: string
  cropped?: string
}

const images = ref<ImageItem[]>([])      // Mảng chứa các ảnh (gốc và đã crop)
const currentIndex = ref(0)              // Ảnh hiện hành đang được crop
const aspectRatio = ref<number>(1)       // Mặc định crop hình vuông (1:1)
const cropper = ref<any>(null)           // Lưu instance của VueCropper

// Computed property trả về ảnh hiện hành
const currentImage = computed(() => images.value[currentIndex.value] || { original: '' })

// Xử lý khi người dùng chọn file (cho phép chọn nhiều ảnh)
const onFileChange = (event: Event) => {
  const target = event.target as HTMLInputElement
  if (target.files && target.files.length) {
    images.value = [] // Reset danh sách ảnh
    const files = Array.from(target.files)
    let loaded = 0
    files.forEach((file) => {
      const reader = new FileReader()
      reader.onload = (e: ProgressEvent<FileReader>) => {
        if (e.target && typeof e.target.result === 'string') {
          console.log('result', e.target.result)
          images.value.push({ original: e.target.result, cropped: '' })
          loaded++
          // Sau khi load xong tất cả ảnh, đặt currentIndex về 0
          if (loaded === files.length) {
            currentIndex.value = 0
          }
        }
      }
      reader.readAsDataURL(file)
    })
  }
}

// Hàm crop ảnh: dùng cropper lấy canvas và chuyển thành dataURL, sau đó lưu vào mảng images
const cropImage = () => {
  if (cropper.value && cropper.value.getCroppedCanvas) {
    const canvas = cropper.value.getCroppedCanvas()
    if (canvas) {
      images.value[currentIndex.value].cropped = canvas.toDataURL('image/png')
    }
  }
}

// Các hàm toolbar:
const setSquare = () => {
  aspectRatio.value = 1
  if (cropper.value && cropper.value.setAspectRatio) {
    cropper.value.setAspectRatio(1)
  }
}

const setRectangle = () => {
  aspectRatio.value = 16 / 9
  if (cropper.value && cropper.value.setAspectRatio) {
    cropper.value.setAspectRatio(16 / 9)
  }
}

const rotateLeft = () => {
  if (cropper.value && cropper.value.rotate) {
    cropper.value.rotate(-90)
  }
}

const rotateRight = () => {
  if (cropper.value && cropper.value.rotate) {
    cropper.value.rotate(90)
  }
}

// Hàm resetCropper: reset lại vị trí ảnh và khung cắt về trạng thái ban đầu
const resetCropper = () => {
  if (cropper.value && cropper.value.reset) {
    cropper.value.reset()
  }
}

// Hàm chuyển sang ảnh tiếp theo
const nextImage = () => {
  if (currentIndex.value < images.value.length - 1) {
    currentIndex.value++
  }
}

// Hàm chuyển về ảnh trước đó
const previousImage = () => {
  if (currentIndex.value > 0) {
    currentIndex.value--
  }
}
</script>
