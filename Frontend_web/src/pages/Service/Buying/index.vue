<template>
  <BaseScreenProduct>
    <template #body>
      <div ref="scrollContainer" @scroll.passive="handleScroll">
        <GradientCard />
        <!-- Featured Products -->
        <div class="mb-16 mx-auto max-w-[1600px] 3xl:max-w-[2000px]">
          <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Featured Products</h2>
          <div class="flex justify-end">
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
            </SlideBar>
          </div>
          <CardContainer>
            <template #body>
              <CardProduct2 :product-model="store.produtList" @on-buy="console.log('a')" />
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
  import { SortBy } from '@PKG_SRC/types/enums/constantBackend'
  import { initCarousels } from 'flowbite'

  const store = useDisplayProductStore()
  const searchStore = useSearchStore()

  const pageSize = ref(5)
  const scrollContainer = ref<HTMLElement | null>(null)

  const fetchProducts = async () => {
    await store.GetProductList(searchStore.searchService, 0, 0, SortBy.MostPopular, pageSize.value)
    await nextTick()
  }

  const loadMore = () => {
    pageSize.value += 5
    fetchProducts()
  }

  const handleScroll = () => {
    if (!scrollContainer.value) return
    const { scrollTop, clientHeight, scrollHeight } = scrollContainer.value
    if (scrollTop + clientHeight >= scrollHeight - 10) loadMore()
  }

  onMounted(async () => {
    fetchProducts()
    initCarousels()
  })
</script>
