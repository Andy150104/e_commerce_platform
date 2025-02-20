<template>
  <div class="relative">
    <div
      id="gradient-card"
      class="bg-gradient-to-r from-blue-100 via-blue-200 to-purple-100 p-8 md:max-w-none md:p-16 lg:p-48 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"
      ref="gradientCard"
    >
      <div class="absolute inset-0 pointer-events-none z-0 gradient-effect" ref="gradientEffect"></div>
      <div class="max-w-3xl mx-auto text-center relative z-10">
        <h1 class="text-4xl font-bold text-gray-900 mb-4">Discover, Trade, and Collect Your Unique Accessories</h1>
        <p class="text-lg text-gray-700 mb-8">
          Unlock a world of surprises with our exclusive blind boxes. Trade and collect accessories effortlessly through our seamless platform
        </p>
        <div>
          <BaseControlSearchOneField/>
        </div>
        <div class="mt-8 flex justify-center space-x-4">
          <button :class="className.BUTTON_GRADIENT_1">Buy Now</button>
          <button :class="className.BUTTON_GRADIENT_2">Exchange now</button>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
  import BaseControlSearch from '@PKG_SRC/components/Basecontrol/BaseControlSearch.vue'
import BaseControlSearchOneField from '@PKG_SRC/components/Basecontrol/BaseControlSearchOneField.vue'
  import { className } from '@PKG_SRC/utils/class/className'

  const gradientCard = ref<HTMLElement | null>(null)
  const gradientEffect = ref<HTMLElement | null>(null)

  const onMouseMove = (e: MouseEvent) => {
    const rect = gradientCard.value!.getBoundingClientRect()
    const x = e.clientX - rect.left
    const y = e.clientY - rect.top

    gradientEffect.value!.style.transform = `translate(${x - 500}px, ${y - 500}px)` // Điều chỉnh tâm gradient
    gradientEffect.value!.style.opacity = '1'
  }
  const onMouseLeave = () => {
    gradientEffect.value!.style.opacity = '0'
  }
  onMounted(async () => {
    await nextTick()
    if (gradientCard.value && gradientEffect.value) {
      gradientCard.value.addEventListener('mousemove', onMouseMove)
      gradientCard.value.addEventListener('mouseleave', onMouseLeave)
    }
  })
  onUnmounted(() => {
    gradientCard.value?.removeEventListener('mousemove', onMouseMove)
    gradientCard.value?.removeEventListener('mouseleave', onMouseLeave)
  })
</script>
<style scoped>
  .gradient-effect {
    background: radial-gradient(circle, rgba(0, 229, 229, 0.41) 0%, rgba(51, 187, 255, 0.1) 100%);
    transform: translate(-50%, -50%);
    width: 1400px; /* Kích thước lớn để tạo hiệu ứng lan tỏa */
    height: 1400px;
    position: absolute;
    border-radius: 50%;
    filter: blur(100px); /* Làm mờ nhẹ nhàng */
    opacity: 0;
    transition:
      opacity 0.3s ease,
      transform 0.1s ease;
  }
  #gradient-card {
    position: relative;
    overflow: hidden;
    clip-path: inset(0);
  }
  #gradient-card:hover .gradient-effect {
    opacity: 1;
  }
</style>