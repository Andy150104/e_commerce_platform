<template>
  <BaseScreenProduct>
    <template #body>
      <!-- Tiêu đề & nút quay lại -->
      <div class="w-full max-w-4xl mx-auto flex items-center px-4 py-3 shadow-md border-b">
  <button @click="goBack" class="text-xl">&#x2190;</button>
  <h2 class="text-lg font-semibold flex-1 text-center">Giỏ hàng của bạn</h2>
</div>


      <div class="min-h-screen bg-gray-900 text-white p-6 pb-20 flex flex-col items-center">
        <div class="max-w-4xl w-full bg-gray-800 p-6 rounded-lg shadow-lg">
          <!-- CardCart-->
          <div class="flex justify-center">
            <CardCart :cart-model="cartStore.cartList" />
          </div>
        </div>
      </div>

      <!-- Thanh toán & Tổng tiền -->
      <div class="fixed bottom-0 left-1/2 transform -translate-x-1/2 w-full max-w-4xl bg-gray-800 p-4 shadow-lg flex justify-between items-center rounded-t-lg">
        <p class="text-lg font-bold">Tạm tính: {{ formatMoney(totalPrice) }}</p>
        <button class="bg-blue-500 px-6 py-3 rounded-lg font-semibold">Thanh toán</button>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script setup lang="ts">
import CardCart from '@PKG_SRC/components/Card/CardCart.vue'
import BaseScreenProduct from '@PKG_SRC/layouts/Basecreen/BaseScreenProduct.vue'
import { useCartStore } from '@PKG_SRC/stores/Modules/Blind_Box/CartStore'
import { computed } from 'vue'

const cartStore = useCartStore()

// Tính tổng tiền
const totalPrice = computed(() => {
  return cartStore.cartList.reduce((sum, item) => sum + (item.totalPrice || 0), 0)
})

// Format tiền tệ VND
const formatMoney = (money: number) => {
  return `${new Intl.NumberFormat('vi-VN').format(money)} VND`
}

// Quay lại trang trước
const goBack = () => {
  window.history.back()
}
</script>
