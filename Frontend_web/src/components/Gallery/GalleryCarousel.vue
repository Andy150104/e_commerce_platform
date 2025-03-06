<template>
  <div class="flex flex-col items-center">
    <!-- Carousel Container -->
    <div class="w-full max-w-4xl relative">
      <div :id="carouselId" class="relative w-full">
        <!-- Carousel wrapper: giữ kích thước hình chữ nhật cố định -->
        <div class="relative h-80 md:h-96 xl:h-[450px] overflow-hidden rounded-xl">
          <!-- Tạo các slide động dựa vào prop images -->
          <div v-for="(image, index) in images" :key="index" :id="`${carouselItemPrefix}${index}`" class="hidden duration-300 ease-in-out">
            <img :src="image.src" :alt="image.alt || `Slide ${index + 1}`" class="w-full h-full object-cover rounded-xl" />
          </div>
        </div>
        <!-- Slider controls -->
        <button
          data-carousel-prev
          type="button"
          class="absolute left-2 top-1/2 -translate-y-1/2 p-3 bg-white/50 rounded-full shadow-md hover:bg-white transition z-30"
          @click="handlePrev"
        >
          <svg class="h-6 w-6 text-gray-800" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 6 10">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 1 1 5l4 4" />
          </svg>
        </button>
        <button
          data-carousel-next
          type="button"
          class="absolute right-2 top-1/2 -translate-y-1/2 p-3 bg-white/50 rounded-full shadow-md hover:bg-white transition z-30"
          @click="handleNext"
        >
          <svg class="h-6 w-6 text-gray-800" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 6 10">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 9 4-4-4-4" />
          </svg>
        </button>
      </div>
    </div>

    <!-- Gallery Thumbnails -->
    <div class="mt-4 grid grid-cols-5 gap-3 w-full max-w-4xl">
      <div v-for="(image, index) in images" :key="index" @click="resetCarouselTo(index)" class="cursor-pointer transition-transform hover:scale-105">
        <!-- Ràng buộc class: nếu activeIndex === index thì viền sẽ là màu đỏ -->
        <img
          :class="['w-full h-3/5 object-cover rounded-lg border', activeIndex === index ? 'border-blue-500 border-2' : 'border-gray-300']"
          :src="image.src"
          :alt="image.alt || `Thumbnail ${index + 1}`"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from 'vue'
  import { Carousel } from 'flowbite'
  import type { ImageItemGallery } from '@PKG_SRC/types'

  // Định nghĩa prop nhận vào mảng ảnh
  const props = defineProps<{
    images: ImageItemGallery[]
  }>()

  // Biến reactive để theo dõi slide đang hoạt động.
  // Lưu ý: nếu bạn muốn slide đầu tiên được chọn, hãy đặt giá trị mặc định là 0;
  // hoặc nếu bạn dùng defaultPosition: 1 trong options thì giá trị mặc định cũng nên là 1.
  const activeIndex = ref(1)

  // Tạo id duy nhất cho carousel để tránh xung đột nếu có nhiều instance
  const uniqueId = Math.random().toString(36).substring(2, 9)
  const carouselId = `carousel-${uniqueId}`
  const carouselItemPrefix = `carousel-item-${uniqueId}-`

  // Biến lưu instance của Carousel
  let carouselInstance: Carousel

  // Hàm cập nhật activeIndex dựa trên slide hiện tại của carousel
  const updateActiveIndex = () => {
    const currentSlide = carouselInstance.getActiveItem()
    if (currentSlide) {
      activeIndex.value = currentSlide.position
    }
  }

  // Hàm xử lý nút prev
  const handlePrev = () => {
    if (carouselInstance) {
      carouselInstance.prev()
      updateActiveIndex()
    }
  }

  // Hàm xử lý nút next
  const handleNext = () => {
    if (carouselInstance) {
      carouselInstance.next()
      updateActiveIndex()
    }
  }

  /**
   * Hàm chuyển carousel đến slide mục tiêu với hiệu ứng mượt.
   * @param targetSlide Số thứ tự slide mục tiêu (0-indexed)
   * @param intervalTime Thời gian giữa các bước chuyển (ms). Mặc định là 20ms.
   */
  const resetCarouselTo = (targetSlide: number, intervalTime = 20) => {
    if (carouselInstance) {
      const currentSlide = carouselInstance.getActiveItem()
      if (!currentSlide) return
      const currentPos = currentSlide.position
      const totalSlides = props.images.length

      // Tính số bước cần chuyển theo kiểu vòng tròn
      const forwardSteps = (targetSlide - currentPos + totalSlides) % totalSlides
      const backwardSteps = (currentPos - targetSlide + totalSlides) % totalSlides

      let steps = 0
      let direction: 'next' | 'prev' = 'next'

      if (forwardSteps <= backwardSteps) {
        direction = 'next'
        steps = forwardSteps
      } else {
        direction = 'prev'
        steps = backwardSteps
      }

      const intervalId = setInterval(() => {
        if (steps > 0) {
          if (direction === 'next') {
            carouselInstance.next()
          } else {
            carouselInstance.prev()
          }
          updateActiveIndex()
          steps--
        } else {
          clearInterval(intervalId)
        }
      }, intervalTime)
    }
  }

  onMounted(() => {
    const carouselElement = document.getElementById(carouselId)
    if (!carouselElement) return

    // Tạo mảng các slide dựa trên danh sách ảnh
    const items = props.images.map((_, index) => {
      const el = document.getElementById(`${carouselItemPrefix}${index}`)
      return { position: index, el: el! }
    })

    // Cấu hình cho carousel (có thể điều chỉnh thêm nếu cần)
    const options = {
      defaultPosition: 1, // Hoặc đặt là 0 nếu bạn muốn slide đầu tiên
      interval: 3000,
    }
    const instanceOptions = {
      id: carouselId,
      override: true,
    }

    // Khởi tạo Carousel từ Flowbite
    carouselInstance = new Carousel(carouselElement, items, options, instanceOptions)

    // Cập nhật activeIndex ban đầu
    updateActiveIndex()
  })
</script>
