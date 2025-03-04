<template>
  <div class="w-full relative">
    <swiper-container class="centered-slide-carousel relative" pagination space-between="50" :slides-per-view="1">
      <swiper-slide v-for="(image, index) in imageList" :key="index">
        <div class="bg-indigo-50 rounded-2xl flex justify-center items-center lg: h-[38rem]" :class="{ 'h-96': !isMobile, 'h-56': isMobile }">
          <img class="w-full h-full rounded-lg object-cover lg: max-w-4xl" :src="image" alt="" />
        </div>
      </swiper-slide>
    </swiper-container>
  </div>
</template>

<script lang="ts" setup>
  import { Swiper as SwiperContainer, SwiperSlide } from 'swiper/vue'
  import 'swiper/swiper-bundle.css'
  import { ref, onMounted } from 'vue'

  // Kiểm tra thiết bị di động
  const isMobile = ref(window.innerWidth <= 768)

  onMounted(() => {
    window.addEventListener('resize', () => {
      isMobile.value = window.innerWidth <= 768
    })
  })
  onUnmounted(() => {
    window.removeEventListener('resize', () => {
      isMobile.value = window.innerWidth <= 768
    })
  })
  const props = defineProps({
    imageList: {
      type: Array as PropType<string[]>, // Chuyển từ Object sang Array vì imageList là danh sách hình ảnh
      required: true,
    },
  })
</script>
