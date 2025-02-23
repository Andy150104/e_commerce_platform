<template>
  <BaseScreenProduct>
    <template #body>
      <div>
        <GradientCard />
        <!-- Featured Products -->
        <div class="mb-16 mx-auto max-w-[1600px] 3xl:max-w-[2000px]">
          <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Featured Products</h2>
          <div class="flex justify-end">
            <SlideBar>
              <template #button_ui>
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
              </template>
            </SlideBar>
          </div>
          <CardContainer>
            <template #body>
              <CardProduct2 :product-model="store.produtList" @on-buy="console.log('a')" />
            </template>
          </CardContainer>
          <div class="text-center mt-8">
            <button :class="className.BUTTON_DEFAULT_WHITE">Explore More</button>
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

  const store = useDisplayProductStore()

  onMounted(async () => {
    store.GetImageList()
    store.GetProductList()
  })
</script>
