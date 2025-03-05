<template>
  <BaseScreenProduct>
    <template #body>
      <div class="pb-8 bg-white md:pb-16 dark:bg-gray-900 antialiased">
        <div class="max-w-screen-2xl px-4 mx-auto 2xl:px-0">
          <!-- Header Section with bottom divider -->
          <div class="border-b-2 border-gray-400 dark:border-gray-800 pb-4 mb-4">
            <h1 class="text-3xl font-semibold text-gray-900 sm:text-2xl dark:text-white mt-3 mb-4">Product Details</h1>
            <BreadCrumb
              :breadcrumbs="[
                { label: 'Service', link: '#' },
                { label: 'Buying', link: 'http://localhost:3000/Service/Buying' },
                { label: 'Projects', link: '#' },
              ]"
            />
          </div>

          <!-- Product Name -->
          <h1 class="text-xl font-semibold text-gray-900 sm:text-2xl dark:text-white my-6">
            {{ store.productDetail.nameAccessory }}
          </h1>

          <!-- Main Grid -->
          <div class="lg:grid lg:grid-cols-3 lg:gap-8 xl:gap-16 mt-4">
            <!-- Gallery Carousel with a right divider on large screens -->
            <div class="shrink-0 max-w-full lg:col-span-2 mx-auto lg:pr-8 border-gray-400 dark:border-gray-800">
              <GalleryCarousel :images="imageGalleryList" />
            </div>

            <!-- Product Info -->
            <div class="mt-6 sm:mt-8 lg:mt-0 lg:col-span-1 bg-gray-100 rounded-md p-6 border-spacing-2 border-b-0">
              <div class="mt-4 sm:items-center sm:gap-4 sm:flex">
                <p class="text-2xl font-extrabold text-gray-900 sm:text-3xl dark:text-white">{{ store.productDetail.price }}</p>
                <div class="flex items-center gap-2 mt-2 sm:mt-0">
                  <div class="flex items-center gap-1">
                    <!-- 5 Star Icons -->
                    <svg
                      class="w-4 h-4 text-yellow-300"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      width="24"
                      height="24"
                      fill="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path
                        d="M13.849 4.22c-.684-1.626-3.014-1.626-3.698 0L8.397 8.387l-4.552.361c-1.775.14-2.495 2.331-1.142 3.477l3.468 2.937-1.06 4.392c-.413 1.713 1.472 3.067 2.992 2.149L12 19.35l3.897 2.354c1.52.918 3.405-.436 2.992-2.15l-1.06-4.39 3.468-2.938c1.353-1.146.633-3.336-1.142-3.477l-4.552-.36-1.754-4.17Z"
                      />
                    </svg>
                    <svg
                      class="w-4 h-4 text-yellow-300"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      width="24"
                      height="24"
                      fill="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path
                        d="M13.849 4.22c-.684-1.626-3.014-1.626-3.698 0L8.397 8.387l-4.552.361c-1.775.14-2.495 2.331-1.142 3.477l3.468 2.937-1.06 4.392c-.413 1.713 1.472 3.067 2.992 2.149L12 19.35l3.897 2.354c1.52.918 3.405-.436 2.992-2.15l-1.06-4.39 3.468-2.938c1.353-1.146.633-3.336-1.142-3.477l-4.552-.36-1.754-4.17Z"
                      />
                    </svg>
                    <svg
                      class="w-4 h-4 text-yellow-300"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      width="24"
                      height="24"
                      fill="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path
                        d="M13.849 4.22c-.684-1.626-3.014-1.626-3.698 0L8.397 8.387l-4.552.361c-1.775.14-2.495 2.331-1.142 3.477l3.468 2.937-1.06 4.392c-.413 1.713 1.472 3.067 2.992 2.149L12 19.35l3.897 2.354c1.52.918 3.405-.436 2.992-2.15l-1.06-4.39 3.468-2.938c1.353-1.146.633-3.336-1.142-3.477l-4.552-.36-1.754-4.17Z"
                      />
                    </svg>
                    <svg
                      class="w-4 h-4 text-yellow-300"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      width="24"
                      height="24"
                      fill="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path
                        d="M13.849 4.22c-.684-1.626-3.014-1.626-3.698 0L8.397 8.387l-4.552.361c-1.775.14-2.495 2.331-1.142 3.477l3.468 2.937-1.06 4.392c-.413 1.713 1.472 3.067 2.992 2.149L12 19.35l3.897 2.354c1.52.918 3.405-.436 2.992-2.15l-1.06-4.39 3.468-2.938c1.353-1.146.633-3.336-1.142-3.477l-4.552-.36-1.754-4.17Z"
                      />
                    </svg>
                    <svg
                      class="w-4 h-4 text-yellow-300"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      width="24"
                      height="24"
                      fill="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path
                        d="M13.849 4.22c-.684-1.626-3.014-1.626-3.698 0L8.397 8.387l-4.552.361c-1.775.14-2.495 2.331-1.142 3.477l3.468 2.937-1.06 4.392c-.413 1.713 1.472 3.067 2.992 2.149L12 19.35l3.897 2.354c1.52.918 3.405-.436 2.992-2.15l-1.06-4.39 3.468-2.938c1.353-1.146.633-3.336-1.142-3.477l-4.552-.36-1.754-4.17Z"
                      />
                    </svg>
                  </div>
                  <p class="text-sm font-medium leading-none text-gray-500 dark:text-gray-400">(5.0)</p>
                </div>
              </div>
              <BaseControlQuantityInput
                ref="userIdInputRef"
                :xml-column="xmlColumns.quantity"
                :maxlength="2"
                :textModel="fieldValues.quantity"
                :disabled="false"
                :err-msg="fieldErrors.quantity"
              />
              <div class="mt-6 sm:gap-4 sm:items-center sm:flex sm:mt-8">
                <a
                  href="#"
                  title=""
                  class="flex items-center justify-center py-2.5 px-5 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-400 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-100 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
                  role="button"
                >
                  <svg
                    class="w-5 h-5 -ms-2 me-2"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <path
                      stroke="currentColor"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      stroke-width="2"
                      d="M12.01 6.001C6.5 1 1 8 5.782 13.001L12.011 20l6.23-7C23 8 17.5 1 12.01 6.002Z"
                    />
                  </svg>
                  Add to favorites
                </a>
                <SlideBar title-side-bar="Cart">
                  <template #button_ui>
                    <!-- Loại bỏ các thuộc tính tự động mở sidebar -->
                    <button
                      class="text-white mt-4 sm:mt-0 bg-primary-700 hover:bg-primary-800 focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 dark:bg-primary-600 dark:hover:bg-primary-700 focus:outline-none dark:focus:ring-primary-800 flex items-center justify-center"
                      type="button"
                      @click="handleAddToCart"
                    >
                      <svg
                        class="w-5 h-5 -ms-2 me-2"
                        aria-hidden="true"
                        xmlns="http://www.w3.org/2000/svg"
                        width="24"
                        height="24"
                        fill="none"
                        viewBox="0 0 24 24"
                      >
                        <path
                          stroke="currentColor"
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M4 4h1.5L8 16m0 0h8m-8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm.75-3H7.5M11 7H6.312M17 4v6m-3-3h6"
                        />
                      </svg>
                      Add to cart
                    </button>
                  </template>
                  <template #body>
                    <CardCart v-if="isCartVisible" :cart-model="cartList" />
                  </template>
                </SlideBar>
              </div>

              <hr class="my-6 md:my-8 border-gray-400 dark:border-gray-800" />

              <p class="mb-6 text-gray-500 dark:text-gray-400">
                {{ store.productDetail.description }}
              </p>
            </div>

            <!-- Accordion with top divider -->
            <div class="md:col-span-2 mt-8 border-t-2 border-gray-400 dark:border-gray-800 pt-8">
              <Accordion />
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script setup lang="ts">
  import type { DPSSelectCartItemEntity } from '@PKG_API/@types'
  import Accordion from '@PKG_SRC/components/Accordion/Accordion.vue'
  import BaseControlQuantityInput from '@PKG_SRC/components/Basecontrol/BaseControlQuantityInput.vue'
  import BreadCrumb from '@PKG_SRC/components/BreadCrumb/BreadCrumb.vue'
  import CardCart from '@PKG_SRC/components/Card/CardCart.vue'
  import GalleryCarousel from '@PKG_SRC/components/Gallery/GalleryCarousel.vue'
  import SlideBar from '@PKG_SRC/components/NavBar/SlideBar.vue'
  import BaseScreenProduct from '@PKG_SRC/layouts/Basecreen/BaseScreenProduct.vue'
  import { useCartStore } from '@PKG_SRC/stores/Modules/Blind_Box/CartStore'
  import { useDetailProductStore } from '@PKG_SRC/stores/Modules/Blind_Box/DetailProductStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { Drawer } from 'flowbite'
  import { useForm } from 'vee-validate'

  const store = useDetailProductStore()
  const cartStore = useCartStore()
  const isCartVisible = ref(true)
  const isUpdated = ref(false)
  const route = useRoute()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
    quantity: XmlLoadColumn({
      id: 'quantity',
      name: 'quantity',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const codeProduct = computed(() => {
    const id = route.params.id
    return Array.isArray(id) ? id[0] : id
  })

  const imageGalleryList = computed(() => {
    return store.convertImageList(store.productDetail.imageUrls || [])
  })

  const addToCart = async () => {
    await store.AddProductToCart(codeProduct.value)
    return true
  }

  const cartList = computed(() => cartStore.cartList)

  const handleAddToCart = async () => {
    const result = await addToCart()
    if (result !== false) {
      await cartStore.GetAllCart()
      isCartVisible.value = false
      await nextTick()
      isCartVisible.value = true
      const drawerElement = document.getElementById('drawer-navigation')
      if (drawerElement) {
        const drawerInstance = new Drawer(drawerElement)
        isUpdated.value = true
        drawerInstance.show()
      }
    }
  }

  onMounted(async () => {
    if (!(await store.GetProductById(codeProduct.value))) {
      console.log(store.convertImageList(store.productDetail.imageUrls || []))
      window.location.href = 'http://localhost:3000/Home'
    }
  })
</script>
