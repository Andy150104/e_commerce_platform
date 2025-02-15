<template>
  <div
    v-for="(product, index) in productModel"
    :key="index"
    class="animate-fade-up max-w-sm md:max-w-[400px] w-full h-[600px] mx-auto bg-white border border-gray-200 rounded-lg shadow-lg overflow-hidden"
  >
    <!-- Carousel Section -->
    <div id="carouselExample" class="relative h-[300px]" data-carousel="static">
      <div class="relative overflow-hidden h-full">
        <!-- Carousel Items -->
        <div v-for="(image, index) in product.imageUrl" :key="index" class="hidden duration-700 ease-in-out" data-carousel-item>
          <img :src="image" :alt="'Image ' + (index + 1)" class="block w-full h-full object-cover" />
        </div>
      </div>
      <!-- Carousel Controls -->
      <button
        type="button"
        class="absolute top-0 left-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
        data-carousel-prev
      >
        <span
          class="inline-flex items-center justify-center w-8 h-8 rounded-full bg-black/30 group-hover:bg-black/50 focus:ring-4 focus:ring-black/60"
        >
          <svg aria-hidden="true" class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7"></path>
          </svg>
        </span>
      </button>
      <button
        type="button"
        class="absolute top-0 right-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
        data-carousel-next
      >
        <span
          class="inline-flex items-center justify-center w-8 h-8 rounded-full bg-black/30 group-hover:bg-black/50 focus:ring-4 focus:ring-black/60"
        >
          <svg aria-hidden="true" class="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"></path>
          </svg>
        </span>
      </button>
    </div>

    <!-- Content Section -->
    <div class="p-6">
      <h5 class="mb-2 text-xl font-semibold tracking-tight text-gray-800">{{ product.nameProduct }}</h5>
      <p class="mb-4 text-sm text-gray-600">{{ product.shortDescription }}</p>
      <div v-if="product.salePrice">
        <p class="text-2xl font-bold text-gray-800 mb-1">{{ moneyFormatter(Number(product.price)) }}</p>
        <p class="text-sm text-gray-500 line-through">{{ moneyFormatter(Number(product.salePrice)) }}</p>
      </div>
      <div v-else>
        <p class="text-2xl font-bold text-gray-800 mb-1">{{ moneyFormatter(Number(product.price)) }}</p>
      </div>
      <div class="flex items-center mb-4">
        <div class="flex items-center mt-3 mb-2">
          <RatingStar :rating="product.rating" />
          <span class="text-gray-600 text-sm ml-2">{{ '(' + formatRating(product.rating) + '/5.0)' }}</span>
        </div>
      </div>
      <div class="flex items-center space-x-4">
        <button class="flex-1 py-2 px-4 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-100">
          Wishlist
        </button>
        <button
          class="flex-1 py-2 px-4 text-sm font-medium text-white bg-blue-600 border border-blue-600 rounded-lg hover:bg-blue-700"
          @click="emit('on-buy')"
        >
          Buy now
        </button>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
  import type { PropType } from 'vue'
  import RatingStar from '../Rating/RatingStar.vue'
  import type { ProductMypp111 } from '@PKG_SRC/stores/Modules/Mypg/Mypg'
  import { initCarousels } from 'flowbite'
  import { Currency, Locale } from '@PKG_SRC/types/enums/constantFrontend'
  const props = defineProps({
    productModel: {
      type: Object as PropType<any[]>,
      required: false,
    },
  })

  const productModel = computed(() => props.productModel)

  const moneyFormatter = (money?: number) => {
    if (money) return formatMoney(money, Currency.VND, Locale.VI_VN)
  }

  const formatRating = (rating: number) => {
    return new Intl.NumberFormat('en-US', { minimumFractionDigits: 1, maximumFractionDigits: 1 }).format(rating)
  }

  interface Emits {
    (e: 'on-buy'): void
  }

  const emit = defineEmits<Emits>()

  onMounted(async () => {
    await nextTick()
    initCarousels()
  })
</script>
