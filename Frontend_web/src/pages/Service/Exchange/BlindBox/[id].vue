<template>
  <BaseScreenProduct>
    <template #body>
      <div class="pb-8 bg-white md:pb-16 dark:bg-gray-900 antialiased">
        <div class="max-w-screen-2xl px-4 mx-auto 2xl:px-0">
          <!-- Header Section with bottom divider -->
          <div class="border-b-2 border-gray-400 dark:border-gray-800 pb-4 mb-4">
            <h1 class="text-3xl font-semibold text-gray-900 sm:text-2xl dark:text-white mt-3 mb-4">Exchange Details</h1>
            <BreadCrumb
              :breadcrumbs="[
                { label: 'Service', link: '#' },
                { label: 'Exchange', link: 'http://localhost:3000/Service/Exchange' },
                { label: 'Blindbox', link: '#' },
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
            <div class="shrink-0 max-w-full lg:col-span-3 mx-auto lg:pr-8 border-gray-400 dark:border-gray-800">
              <GalleryCarousel :images="imageGalleryList" />
              <!-- Product Info -->
            </div>
            <div class="mt-6 sm:mt-8 col-span-3 lg:mt-0 bg-gray-100 rounded-md p-6 border-spacing-2 border-b-0 break-words">
                <div class="mt-4 sm:items-center sm:gap-4 sm:flex">
                  <p class="text-2xl font-extrabold text-gray-900 sm:text-3xl dark:text-white">{{ store.productDetail.price }}</p>
                </div>
                <div class="mt-6 sm:gap-4 sm:items-center sm:flex sm:mt-8">
                  <ModalInputForm
                    :header="'Exchange Info'"
                    :button-icon="'pi pi-plus'"
                    :button-label="'Exchange Now'"
                    :button-severity="'contrast'"
                    :width="'80rem'"
                  />
                </div>

                <hr class="my-6 md:my-8 border-gray-400 dark:border-gray-800" />
              </div>
            <!-- Accordion with top divider -->
            <div class="md:col-span-3 mt-8 border-t-2 border-gray-400 dark:border-gray-800 pt-8">
              <div
                v-html="store.productDetail.description"
                class="mb-6 text-gray-500 dark:text-gray-400 whitespace-normal break-words overflow-auto"
              ></div>
              <Accordion />
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script setup lang="ts">
  import Accordion from '@PKG_SRC/components/Accordion/Accordion.vue'
  import BaseControlQuantityInput from '@PKG_SRC/components/Basecontrol/BaseControlQuantityInput.vue'
  import BreadCrumb from '@PKG_SRC/components/BreadCrumb/BreadCrumb.vue'
  import CardCart from '@PKG_SRC/components/Card/CardCart.vue'
  import GalleryCarousel from '@PKG_SRC/components/Gallery/GalleryCarousel.vue'
  import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue'
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
    //   if (!(await store.GetProductById(codeProduct.value))) {
    //     console.log(store.convertImageList(store.productDetail.imageUrls || []))
    //     window.location.href = 'http://localhost:3000/Home'
    //   }
  })
</script>
