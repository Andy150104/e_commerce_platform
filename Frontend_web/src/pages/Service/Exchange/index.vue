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
          <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Exchange Products</h2>
          <div class="flex justify-end gap-2 mb-4">
            <BaseControlSelectInput :xml-column="xmlColumns.sortBy" :model="fieldValues.sortBy" :disabled="false" :master-name="'SortBy'" />
          </div>
          <CardContainer>
            <template #body>
              <CardProduct2 ref="cardRef" :is-show-wish-list="false" :is-exchange="true" :product-model="store.ExchangeList" :label-name="'Exchange now'" @on-buy="onBuyNow" :is-have-side-bar="true">
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
import { useExchangeStore } from '@PKG_SRC/stores/Modules/Blind_Box/ExchangeStore'

  const store = useDisplayProductStore()
  const exchangeStore = useExchangeStore()
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
    await store.GetProductList(SearchService.BlindBox, 0, 0, store.fieldValues.sortBy, pageSize.value, searchStore.searchParams)
    await nextTick()
  }

  const onReturnLogin = () => {
    window.location.href = 'http://localhost:3000/Login'
  }

  const onBuyNow = async (selectedProduct: any) => {
    // formMessageStore.SetFormMessageNotApiRes('E00001', true, onReturnLogin)
    if (authStore.isAuthorization) {
      const router = useRouter()
      router.push(`/Service/Exchange/BlindBox/${selectedProduct}`)
      return
    }
    isShowPopupRef.value.closePopup(true)
  }

  const handleSearch = () => {
    searchStore.searchParams = searchOneFieldRef.value.onGetSearch()
    router.push({
      path: '/Service/Exchange',
      query: { search: searchStore.searchParams },
    })
  }

  watch(
    () => store.fieldValues.sortBy,
    async (newVal, oldVal) => {
      if (newVal !== oldVal) {
        store.ExchangeList = []
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
