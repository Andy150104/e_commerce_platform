<template>
  <BaseScreenProduct @on-scroll-load="handleScroll">
    <template #body>
      <div ref="scrollContainer" @scroll.passive="handleScroll">
        <ModalNotButton
          ref="isShowPopupRef"
          button-cancel-name="Cancel"
          button-o-k-name="Yes, Move to Login!!!"
          message="You must to login to countinue to buy"
          @on-click="onReturnLogin"
        />
        <GradientCard>
          <template #search>
            <BaseControlSearchOneField ref="searchOneFieldRef" @on-click-search="handleSearch" />
          </template>
        </GradientCard>
        <!-- Featured Products -->
        <div class="mb-16 mx-auto max-w-[1600px] 3xl:max-w-[2000px]">
          <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Featured Products</h2>
          <div class="flex justify-end gap-2 mb-4">
            <SlideBar>
              <template #button_ui>
                <button
                  class="flex items-center gap-2 text-gray-900 bg-white border border-gray-300 hover:bg-gray-100 focus:ring-4 focus:ring-gray-200 font-medium rounded-lg text-sm px-5 py-2.5 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
                  type="button"
                  data-drawer-target="drawer-navigation"
                  data-drawer-show="drawer-navigation"
                  aria-controls="drawer-navigation"
                >
                  <svg
                    className="w-4 h-4 text-gray-700 dark:text-gray-300"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 20 20"
                  >
                    <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M3 5h14M5 10h10m-7 5h4" />
                  </svg>
                  Filters
                  <svg
                    className="w-4 h-4 text-gray-700 dark:text-gray-300"
                    aria-hidden="true"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 20 20"
                  >
                    <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 8l4 4 4-4" />
                  </svg>
                </button>
              </template>
              <template #body>
                <div>
                  <div class="bg-white p-6 space-y-6">
                    <!-- Hàng 1: Product Brand, Product Model -->
                    <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
                      <!-- Product Brand -->
                      <div>
                        <label for="productBrand" class="block text-sm font-medium mb-1">Product Brand</label>
                        <select
                          id="productBrand"
                          class="block w-full rounded-md border-gray-300 dark:border-gray-700 bg-gray-50 dark:bg-gray-700 focus:ring-blue-500 focus:border-blue-500 dark:focus:ring-blue-400 dark:focus:border-blue-400 text-sm p-2"
                        >
                          <option value="Apple">Apple</option>
                          <option value="Samsung">Samsung</option>
                          <option value="Asus">Asus</option>
                          <option value="Lenovo">Lenovo</option>
                        </select>
                      </div>
                      <!-- Product Model -->
                      <div>
                        <label for="productModel" class="block text-sm font-medium mb-1">Product Model</label>
                        <select
                          id="productModel"
                          class="block w-full rounded-md border-gray-300 dark:border-gray-700 bg-gray-50 dark:bg-gray-700 focus:ring-blue-500 focus:border-blue-500 dark:focus:ring-blue-400 dark:focus:border-blue-400 text-sm p-2"
                        >
                          <option value="iMac 27">iMac 27</option>
                          <option value="MacBook Pro">MacBook Pro</option>
                          <option value="iPhone 14">iPhone 14</option>
                          <option value="iPad Air">iPad Air</option>
                        </select>
                      </div>
                    </div>

                    <!-- Hàng 2: (ví dụ) Manufacture Year, Price Range -->
                    <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
                      <!-- Manufacture Year (nếu cần) -->
                      <div>
                        <label class="block text-sm font-medium mb-1">Manufacture Year</label>
                        <div class="flex items-center space-x-2">
                          <input
                            type="number"
                            placeholder="Start year"
                            class="w-1/2 rounded-md border-gray-300 dark:border-gray-700 bg-gray-50 dark:bg-gray-700 focus:ring-blue-500 focus:border-blue-500 dark:focus:ring-blue-400 dark:focus:border-blue-400 text-sm p-2"
                          />
                          <input
                            type="number"
                            placeholder="End year"
                            class="w-1/2 rounded-md border-gray-300 dark:border-gray-700 bg-gray-50 dark:bg-gray-700 focus:ring-blue-500 focus:border-blue-500 dark:focus:ring-blue-400 dark:focus:border-blue-400 text-sm p-2"
                          />
                        </div>
                      </div>
                      <!-- Price Range -->
                      <div>
                        <label class="block text-sm font-medium mb-1">Price Range</label>
                        <div class="flex items-center space-x-2">
                          <input
                            type="number"
                            placeholder="From"
                            class="w-1/2 rounded-md border-gray-300 dark:border-gray-700 bg-gray-50 dark:bg-gray-700 focus:ring-blue-500 focus:border-blue-500 dark:focus:ring-blue-400 dark:focus:border-blue-400 text-sm p-2"
                          />
                          <input
                            type="number"
                            placeholder="To"
                            class="w-1/2 rounded-md border-gray-300 dark:border-gray-700 bg-gray-50 dark:bg-gray-700 focus:ring-blue-500 focus:border-blue-500 dark:focus:ring-blue-400 dark:focus:border-blue-400 text-sm p-2"
                          />
                        </div>
                      </div>
                    </div>

                    <!-- Condition -->
                    <div>
                      <label class="block text-sm font-medium mb-2">Condition</label>
                      <div class="flex items-center space-x-4">
                        <label class="flex items-center space-x-1 text-sm cursor-pointer">
                          <input type="radio" name="condition" value="All" class="text-blue-500 focus:ring-blue-500 dark:focus:ring-blue-400" />
                          <span>All</span>
                        </label>
                        <label class="flex items-center space-x-1 text-sm cursor-pointer">
                          <input type="radio" name="condition" value="New" class="text-blue-500 focus:ring-blue-500 dark:focus:ring-blue-400" />
                          <span>New</span>
                        </label>
                        <label class="flex items-center space-x-1 text-sm cursor-pointer">
                          <input type="radio" name="condition" value="Used" class="text-blue-500 focus:ring-blue-500 dark:focus:ring-blue-400" />
                          <span>Used</span>
                        </label>
                      </div>
                    </div>

                    <!-- Rating (dùng radio để chọn 1-5 sao) -->
                    <div>
                      <label class="block text-sm font-medium mb-2">Rating</label>
                    </div>

                    <!-- Nút Apply và Clear all -->
                    <div class="flex items-center justify-end space-x-3">
                      <button
                        class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 dark:focus:ring-blue-400 text-sm"
                      >
                        Apply filters
                      </button>
                      <button
                        class="px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700 text-sm"
                      >
                        Clear all
                      </button>
                    </div>
                  </div>
                </div>
              </template>
            </SlideBar>
            <BaseControlSelectInput :xml-column="xmlColumns.sortBy" :model="fieldValues.sortBy" :disabled="false" :master-name="'SortBy'" />
          </div>
          <CardContainer>
            <template #body>
              <CardProduct2 ref="cardRef" :product-model="store.produtList" :label-name="'Exchange now'" @on-buy="onBuyNow" :is-have-side-bar="true">
                <template #body>
                  <CardCart :cart-model="cartStore.cartList" />
                </template>
              </CardProduct2>
              <CardSkeleton v-if="store.isLoadingSkeletonCard" />
            </template>
          </CardContainer>
          <div class="text-center mt-8">
            <button :class="className.BUTTON_DEFAULT_WHITE" @click="loadMore">Explore More</button>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script lang="ts" setup>
  import { ref, onMounted, onUnmounted } from 'vue'
  import { className } from '@PKG_SRC/utils/class/className'
  import CardProduct2 from '@PKG_SRC/components/Card/CardProduct2.vue'
  import CardContainer from '@PKG_SRC/layouts/CardContainer/CardContainer.vue'
  import SlideBar from '@PKG_SRC/components/NavBar/SlideBar.vue'
  import { useDisplayProductStore } from '@PKG_SRC/stores/Modules/Blind_Box/DisplayProductStore'
  import BaseScreenProduct from '@PKG_SRC/layouts/Basecreen/BaseScreenProduct.vue'
  import GradientCard from '@PKG_SRC/layouts/GradientCard/GradientCard.vue'
  import { useSearchStore } from '@PKG_SRC/stores/master/searchStore'
  import { SearchService, SortBy } from '@PKG_SRC/types/enums/constantBackend'
  import { initCarousels } from 'flowbite'
  import CardSkeleton from '@PKG_SRC/components/Skeleton/CardSkeleton.vue'
  import { useLoadingStore } from '@PKG_SRC/stores/Modules/usercontrol/loadingStore'
  import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
  import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
  import ModalNotButton from '@PKG_SRC/components/Modal/ModalNotButton.vue'
  import { useDetailProductStore } from '@PKG_SRC/stores/Modules/Blind_Box/DetailProductStore'
  import CardCart from '@PKG_SRC/components/Card/CardCart.vue'
  import { useCartStore } from '@PKG_SRC/stores/Modules/Blind_Box/CartStore'
  import BaseControlSelectInput from '@PKG_SRC/components/Basecontrol/BaseControlSelectInput.vue'
  import { useForm } from 'vee-validate'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import BaseControlSearchOneField from '@PKG_SRC/components/Basecontrol/BaseControlSearchOneField.vue'

  const store = useDisplayProductStore()
  const searchStore = useSearchStore()
  const cartStore = useCartStore()
  const detailProductStore = useDetailProductStore()
  const formMessageStore = useFormMessageStore()
  const authStore = useAuthStore()
  const cardRef = ref()
  const isShowPopupRef = ref()
  const searchOneFieldRef = ref()
  const isLoading = ref(true)
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const router = useRouter()

  const xmlColumns = {
    sortBy: XmlLoadColumn({
      id: 'sortBy',
      name: 'sortBy',
      rules: '',
      visible: true,
      option: '',
    }),
  }

  const pageSize = ref(5)
  const scrollContainer = ref<HTMLElement | null>(null)

  const fetchProducts = async () => {
    await store.GetProductList(searchStore.searchService, 0, 0, store.fieldValues.sortBy, pageSize.value, searchStore.searchParams)
    await nextTick()
  }

  const onReturnLogin = () => {
    window.location.href = 'http://localhost:3000/Login'
  }

  const onBuyNow = async (selectedProduct: any) => {
    // formMessageStore.SetFormMessageNotApiRes('E00001', true, onReturnLogin)
    if (authStore.isAuthorization) {
      await detailProductStore.AddProductToCart(selectedProduct)
      cardRef.value.openSideBar(true)
      await cartStore.GetAllCart()
      return
    }
    isShowPopupRef.value.closePopup(true)
  }

  const handleSearch = () => {
    searchStore.searchParams = searchOneFieldRef.value.onGetSearch()
    router.push({
      path: '/Service/Buying',
      query: { search: searchStore.searchParams },
    })
  }

  watch(
    () => store.fieldValues.sortBy,
    async (newVal, oldVal) => {
      if (newVal !== oldVal) {
        store.produtList = []
        store.fields.setFieldValue('sortBy', newVal)
        await fetchProducts()
        initCarousels()
      }
    }
  )

  const loadMore = async () => {
    pageSize.value += 5
    await fetchProducts()
    initCarousels()
  }

  const handleScroll = () => {
    if (!scrollContainer.value) return
    const { scrollTop, clientHeight, scrollHeight } = scrollContainer.value
    console.log({ scrollTop, clientHeight, scrollHeight })
    if (scrollTop + clientHeight >= scrollHeight - 10) loadMore()
  }

  onMounted(async () => {
    await fetchProducts()
    initCarousels()
    isLoading.value = false
  })

  onUnmounted(() => {
    store.$reset()
  })
</script>
