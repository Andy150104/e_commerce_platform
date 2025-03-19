<template>
  <BaseScreenDashBoard>
    <template #body>
      <h2 class="text-2xl font-bold text-center text-gray-900 mb-8 dark:text-white">Your Wishlist</h2>
      <CardContainer>
        <template #body>
          <CardProduct2
            ref="cardRef"
            :is-show-wish-list="false"
            :is-show-button2="true"
            :product-model="store.produtList"
            :label-name="'View'"
            @on-function-but2="onFunctionButton2"
            @on-buy="onBuyNow"
            :is-have-side-bar="true"
            :button2-name="'Remove Wishlist'"
          >
          </CardProduct2>
          <CardSkeleton v-if="store.isLoadingSkeletonCard" />
        </template>
      </CardContainer>
    </template>
  </BaseScreenDashBoard>
</template>
<script setup lang="ts">
  import CardProduct2 from '@PKG_SRC/components/Card/CardProduct2.vue'
  import CardSkeleton from '@PKG_SRC/components/Skeleton/CardSkeleton.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import CardContainer from '@PKG_SRC/layouts/CardContainer/CardContainer.vue'
  import { useWishListStore } from '@PKG_SRC/stores/Modules/WishList/WishlistStore'
  import { initCarousels } from 'flowbite'

  const store = useWishListStore()

  const onBuyNow = async (selectedProduct: any) => {
    const router = useRouter()
    router.push(`/Service/Buying/Product/${selectedProduct}`)
  }

  const onFunctionButton2 = async (selectedProduct: any) =>{
    await store.RemoveItemOutWishlist(selectedProduct.codeProduct)
    await store.GetProductWishlist()

  }

  onMounted(async () => {
    await store.GetProductWishlist()
    initCarousels()
  })
</script>
