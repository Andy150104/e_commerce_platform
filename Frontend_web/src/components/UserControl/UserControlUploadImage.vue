<template>
  <div>
    <div class="w-full p-6 bg-white dark:bg-gray-800 rounded-lg shadow-lg">
      <div class="mb-4">
        <label for="file-upload" class="block text-base font-semibold mb-2 dark:text-white">Upload Image</label>
        <div
          v-if="store.uploadImage.length < maxNumberImage"
          class="relative border-2 border-dashed border-gray-300 dark:border-gray-700 rounded-lg p-6 text-center cursor-pointer hover:border-blue-500"
          @drop.prevent="handleFileDrop"
          @dragover.prevent
        >
          <input
            type="file"
            id="file-upload"
            accept="image/*"
            multiple
            class="absolute inset-0 w-full h-full opacity-0 cursor-pointer"
            @change="onFileSelected"
          />
          <p class="text-gray-600 dark:text-gray-400">
            Kéo thả (Drag & Drop) hoặc
            <span class="text-blue-500 underline">chọn file</span> để upload{{ '(tối đa ' + props.maxNumberImage + '  ảnh)' }}
          </p>
        </div>
      </div>

      <!-- Danh sách các ảnh đã chọn -->
      <div
        v-for="(image, index) in store.uploadImage"
        :key="index"
        class="relative w-full max-w-md p-4 mb-4 bg-slate-300 dark:bg-gray-800 rounded-lg shadow-lg"
      >
        <!-- Thông tin ảnh -->
        <div class="flex items-center">
          <div @mouseover="togglePopover(index)" @mouseleave="togglePopover(index)">
            <h1 class="text-sm font-medium text-gray-700 dark:text-gray-300 cursor-pointer lg:text-lg">{{ image.name }}</h1>
          </div>
          <span class="ml-4 text-sm text-gray-600 dark:text-gray-400">{{ image.size }} MB</span>
          <button class="ml-auto text-gray-500 hover:text-gray-700 dark:hover:text-gray-300" @click="removeImage(index)">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>

        <!-- Popover -->
        <div
          v-show="showPopovers[index] && isShowPopover"
          class="absolute z-10 w-[280px] p-3 text-sm text-gray-500 bg-white border border-gray-200 rounded-lg shadow dark:text-gray-400 dark:border-gray-600 dark:bg-gray-800 translate-y-5 -translate-x-2"
        >
          <div class="mb-2 font-semibold text-gray-900 dark:text-white">Image Info</div>
          <img :src="image.imagePreview" alt="" />
          <p><strong>Name:</strong> {{ image.name }}</p>
          <p><strong>Size:</strong> {{ image.size }} MB</p>
        </div>
      </div>
      <div class="flex justify-end mt-4">
        <button
          class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          @click="addAndEdit"
        >
          Edit
        </button>
      </div>
    </div>
    <ModelFullScreen ref="isOpenModal" :label="'Edit'" :is-show-button="false" :is-open-modal="isOpen">
      <template #body>
        <ImageCrop :images="imagesEdit" @updateCrop="updateStoreCrop" />
      </template>
    </ModelFullScreen>
  </div>
</template>

<script setup lang="ts">
  import ImageCrop from '@PKG_SRC/components/CropImage/ImageCrop.vue'
  import ModelFullScreen from '@PKG_SRC/components/Modal/ModelFullScreen.vue'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  import type { ImageItem } from '@PKG_SRC/types/models/imageTypes'
  import { ref } from 'vue'

  const props = defineProps({
    maxNumberImage: {
      type: Number,
      required: true,
      default: 5,
    },
    isShowPopover: {
      type: Boolean,
      required: false,
      default: true,
    },
    label: {
      type: String,
      required: false,
      default: 'Upload Image',
    },
  })

  // Danh sách ảnh và trạng thái popover
  const store = useUploadImageStore()
  const imagesEdit = ref<ImageItem[]>([])
  const showPopovers = ref<boolean[]>([])
  const isOpen = ref(false)
  const isOpenModal = ref()

  // Xử lý drag & drop
  const handleFileDrop = (e: DragEvent) => {
    if (!e.dataTransfer?.files) return
    const fileList = Array.from(e.dataTransfer.files)
    handleFiles(fileList)
  }

  // Xử lý chọn file
  const onFileSelected = (e: Event) => {
    const target = e.target as HTMLInputElement
    if (!target.files) return
    const fileList = Array.from(target.files)
    if (fileList.length > props.maxNumberImage) return
    handleFiles(fileList)
    target.value = ''
  }

  // Hàm xử lý file
  function handleFiles(fileList: File[]) {
    let currentIndex = store.uploadImage.length
    if (currentIndex === props.maxNumberImage) return
    for (const file of fileList) {
      const sizeMB = +(file.size / 1024 / 1024).toFixed(2)
      const reader = new FileReader()
      reader.onload = (evt) => {
        const base64 = evt.target?.result as string
        store.SetUploadImage(`image${currentIndex + 1}.png`, sizeMB, base64)
        showPopovers.value.push(false) // Khởi tạo trạng thái popover
        currentIndex++
      }
      reader.readAsDataURL(file)
    }
  }
  // Toggle trạng thái popover
  const togglePopover = (index: number) => {
    showPopovers.value[index] = !showPopovers.value[index]
  }

  // Xóa ảnh
  const removeImage = (index: number) => {
    store.uploadImage.splice(index, 1)
    showPopovers.value.splice(index, 1)
  }

  const addAndEdit = async () => {
    imagesEdit.value = store.uploadImage.map((image) => ({
      original: image.imagePreview,
      cropped: '',
    }))
    isOpenModal.value.isOpenModal(true)
    await nextTick()
  }
  const updateStoreCrop = ({ index, cropped }: { index: number; cropped: string }) => {
    // Giả sử thứ tự của ảnh trong imagesEdit trùng với thứ tự trong store.uploadImage
    store.uploadImage[index].imagePreview = cropped
  }
</script>
