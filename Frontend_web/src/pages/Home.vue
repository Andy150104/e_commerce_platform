<template>
  <BaseScreenHome>
    <template #body>
      <div>
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
              <BaseControlSearch />
            </div>
            <div class="mt-8 flex justify-center space-x-4">
              <button :class="className.BUTTON_GRADIENT_1">Buy Now</button>
              <button :class="className.BUTTON_GRADIENT_2">Exchange now</button>
            </div>
          </div>
        </div>
        <!-- Featured Products -->
        <div class="mb-16 mx-auto max-w-[1600px]">
          <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Featured Products</h2>
          <CardContainer>
            <template #body>
              <CardProduct2 :product-model="store.produtList" />
            </template>
          </CardContainer>
          <div class="text-center mt-8">
            <button :class="className.BUTTON_DEFAULT_WHITE">Explore More</button>
          </div>
        </div>
        <!-- Description block -->
        <div class="bg-gray-100 p-8 rounded-xl mb-16">
          <div class="grid grid-cols-1 lg:grid-cols-2 gap-8 items-center">
            <div class="flex flex-col justify-center text-center lg:text-left">
              <h2 class="text-2xl font-semibold text-gray-900 mb-4">
                Seamless and Transparent Product<br />
                Exchange System
              </h2>
              <p class="text-lg text-gray-700 mb-8">
                Connect with others to exchange products effortlessly. Simply upload the item you want to trade, browse and choose what you love from
                others, and complete the transaction in just a few simple steps. Our platform ensures safety and transparency for every exchange!
              </p>
            </div>
            <div>
              <Carousel :image-list="store.imageList.imagesList" />
            </div>
          </div>
        </div>
        <!-- Plan -->
        <div class="mb-16 mx-auto max-w-[1600px]">
          <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Featured Products</h2>
          <CardPlan />
        </div>
      </div>
    </template>
  </BaseScreenHome>
</template>

<script lang="ts" setup>
  import { ref, onMounted, onUnmounted } from 'vue'
  import { className } from '@PKG_SRC/utils/class/className'
  import BaseControlSearch from '@PKG_SRC/components/Basecontrol/BaseControlSearch.vue'
  import BaseScreenHome from '@PKG_SRC/layouts/Basecreen/BaseScreenHome.vue'
  import CardProduct2 from '@PKG_SRC/components/Card/CardProduct2.vue'
  import CardContainer from '@PKG_SRC/layouts/CardContainer/CardContainer.vue'
  import { useMypgStore } from '@PKG_SRC/stores/Modules/Mypg/Mypg'
  import { useTestStore } from '@PKG_SRC/stores/Modules/Mypg/testStore'
  import CardPlan from '@PKG_SRC/components/Card/CardPlan.vue'
  import Carousel from '@PKG_SRC/components/Carousel/Carousel.vue'

  const gradientCard = ref<HTMLElement | null>(null)
  const gradientEffect = ref<HTMLElement | null>(null)
  const store = useMypgStore()
  const testStore = useTestStore()

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
    store.GetImageList()
    store.GetProductList()
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
