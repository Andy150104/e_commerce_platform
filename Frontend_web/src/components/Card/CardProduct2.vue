<template>
  <div
    v-for="(product, index) in productModel"
    :key="index"
    class="flex flex-col animate-fade-up max-w-sm md:max-w-[400px] w-full h-[600px] mx-auto bg-white border border-gray-200 rounded-lg shadow-lg overflow-hidden cursor-pointer hover:shadow-2xl hover:scale-105 transition-all duration-300"
    @click="goToProduct(product.codeProduct)"
  >
    <!-- Carousel Section -->
    <div id="carouselExample" class="relative h-[300px]" data-carousel="static">
      <div class="relative overflow-hidden h-full">
        <!-- Carousel Items -->
        <div v-for="(image, index) in product.imageUrl" :key="index" class="hidden duration-700 ease-in-out" data-carousel-item>
          <img :src="image.imageUrl" :alt="'Image ' + (index + 1)" class="block w-full h-full object-cover" />
        </div>
      </div>
      <!-- Carousel Controls -->
      <button
        type="button"
        class="absolute top-0 left-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
        data-carousel-prev
        @click.stop=""
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
        @click.stop=""
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
    <div class="p-6 flex flex-col flex-1">
      <h5 class="mb-2 text-xl font-semibold tracking-tight text-gray-800">{{ product.nameAccessory }}</h5>
      <h5 class="mb-2 text-xl font-semibold tracking-tight text-gray-800">{{ product.exchangeName }}</h5>
      <div class="mb-4">
        <p class="text-sm text-gray-600">{{ product.shortDescription ?? "Don't have short description" }}</p>
      </div>
      <div v-if="product.salePrice">
        <!-- <p class="text-2xl font-bold text-gray-800 mb-1">{{ moneyFormatter(Number(product.price)) }}</p> -->
        <p class="text-2xl font-bold text-gray-800 mb-1">{{ moneyFormatter(Number(product.salePrice)) }}</p>
        <!-- <p class="text-sm text-gray-500 line-through">{{ moneyFormatter(Number(product.salePrice)) }}</p> -->
        <p class="text-xl text-gray-500 line-through">{{ product.price }}</p>
      </div>
      <div v-else>
        <p class="text-2xl font-bold text-gray-800 mb-1">{{ product.price }}</p>
        <!-- <p class="text-2xl font-bold text-gray-800 mb-1">{{ moneyFormatter(Number(product.price)) }}</p> -->
      </div>
      <div class="flex items-center mb-4">
        <div class="flex items-center mt-3 mb-2">
          <RatingStar :rating="product.averageRating" />
          <span class="text-gray-600 text-sm ml-2">{{ '(' + formatRating(product.averageRating ?? 0) + '/5.0)' }}</span>
        </div>
      </div>
      <div class="text-gray-600 text-lg">
          <p v-if="product.firstNameCreator || product.lastNameCreator ">Create By: {{ product.firstNameCreator + ' ' + product.lastNameCreator }}</p>
        </div>
      <div class="flex items-center space-x-4 mt-auto">
        <button
          v-if="isShowButton2"
          class="flex-1 py-2 px-4 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-lg hover:bg-gray-100"
          @click.stop="emit('on-function-but2', product)"
        >
          {{ button2Name }}
        </button>
        <button
          class="flex-1 py-2 px-4 text-sm font-medium text-white bg-blue-600 border border-blue-600 rounded-lg hover:bg-blue-700"
          @click.stop="emit('on-buy', product.codeProduct)"
        >
          {{ labelName }}
        </button>
        <button
          v-if="isShowWishList"
          type="button"
          data-tooltip-target="tooltip-add-to-favorites"
          :class="[
            'rounded-lg p-2',
            product.wishListId ? 'bg-red-500 text-white' : 'text-gray-500 hover:bg-red-500 hover:text-gray-900',
            'dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white',
          ]"
          @click.stop="onAddAndRemoveWishlist(product)"
        >
          <span class="sr-only"> Add to Favorites </span>
          <svg class="h-5 w-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
            <path
              stroke="currentColor"
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M12 6C6.5 1 1 8 5.8 13l6.2 7 6.2-7C23 8 17.5 1 12 6Z"
            />
          </svg>
        </button>
        <div
          id="tooltip-add-to-favorites"
          role="tooltip"
          class="tooltip invisible absolute z-10 inline-block rounded-lg bg-gray-900 px-3 py-2 text-sm font-medium text-white opacity-0 shadow-sm transition-opacity duration-300 dark:bg-gray-700"
          data-popper-placement="top"
        >
          Add to favorites
          <div class="tooltip-arrow" data-popper-arrow=""></div>
        </div>
      </div>
    </div>
  </div>
  <div v-if="isHaveSideBar">
    <SideBarNoButton :is-cart="true" text-title="Cart" v-if="isShowSideBar" @on-click-close="handleClose">
      <template #bodySideBar>
        <slot name="body"></slot>
      </template>
    </SideBarNoButton>
  </div>
</template>
<script setup lang="ts">
  import type { PropType } from 'vue'
  import RatingStar from '../Rating/RatingStar.vue'
  import { initCarousels, initFlowbite, initPopovers, initTooltips } from 'flowbite'
  import { Currency, Locale } from '@PKG_SRC/types/enums/constantFrontend'
  import SideBarNoButton from '../NavBar/SideBarNoButton.vue'

  const isShowSideBar = ref(false)

  const props = defineProps({
    isShowButton2: {
      type: Boolean,
      required: false,
      default: false,
    },
    button2Name: {
      type: String,
      required: false,
      default: 'Wishlist',
    },
    productModel: {
      type: Array as PropType<any[]>,
      required: false,
    },
    labelName: {
      type: String,
      required: false,
      default: 'Add',
    },
    isShowRightSideBar: {
      type: Boolean,
      required: false,
      default: true,
    },
    isHaveSideBar: {
      type: Boolean,
      required: false,
      default: false,
    },
    isExchange:{
      type: Boolean,
      required: false,
      default: false
    },
    isShowWishList:{
      type: Boolean,
      required: false,
      default: false
    }
  })

  const isOpen = ref(false)

  const productModel = computed(() => props.productModel)

  const moneyFormatter = (money?: number) => {
    if (money) return formatMoney(money, Currency.VND, Locale.VI_VN)
  }

  const formatRating = (rating: number) => {
    return new Intl.NumberFormat('en-US', { minimumFractionDigits: 1, maximumFractionDigits: 1 }).format(rating)
  }

  const goToProduct = (codeProduct: string) => {
    const router = useRouter()
    props.isExchange ? router.push(`/Service/Exchange/Blindbox/${codeProduct}`) : router.push(`/Service/Buying/Product/${codeProduct}`)
  }

  interface Emits {
    (e: 'on-buy', codeProduct: any): void
    (e: 'on-function-but2', product: any): void
    (e: 'on-add-wishlist', codeProduct: any): void
    (e: 'on-remove-wishlist', codeProduct: any): void
  }

  const onAddAndRemoveWishlist = async (product: any) =>{
    product.wishListId ? emit('on-remove-wishlist', product.codeProduct) : emit('on-add-wishlist', product.codeProduct)
  }

  function openSideBar(value: boolean) {
    isShowSideBar.value = value
  }

  const handleClose = (value: boolean) => {
    isShowSideBar.value = value
  }

  const emit = defineEmits<Emits>()

  onMounted(async () => {
    initFlowbite()
    initCarousels()
    initTooltips()
  })

  defineExpose({
    openSideBar,
  })
</script>
