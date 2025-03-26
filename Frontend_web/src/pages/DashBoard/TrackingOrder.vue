<template>
  <BaseScreenDashBoard>
    <template #body>
      <div class="mx-auto max-w-5xl p-4 sm:p-6 lg:p-8">
        <!-- Nút "Quay lại" hoặc "Lịch sử đơn hàng" -->
        <div class="mb-4">
          <router-link to="/DashBoard/HistoryOrder" class="inline-flex items-center rounded-md bg-blue-50 px-3 py-2 text-sm font-medium text-blue-600 hover:bg-blue-100 transition">
            <!-- Icon mũi tên (Heroicons) -->
            <svg xmlns="http://www.w3.org/2000/svg" class="mr-1 h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7" />
            </svg>
            Back to History order
          </router-link>
        </div>
        <!-- Tiêu đề trang -->
        <h1 class="mb-6 text-2xl font-bold text-gray-800">Chi Tiết Đơn Hàng #123456</h1>
        <!-- Khối thông tin chung của đơn hàng -->
        <div class="mb-6 grid gap-6 sm:grid-cols-2">
          <!-- Thông tin đơn hàng -->
          <div class="rounded-lg bg-white p-4 shadow">
            <h2 class="mb-2 text-lg font-semibold text-gray-700">Thông tin đơn hàng</h2>
            <div class="text-sm text-gray-600 space-y-1">
              <p>Ngày đặt: <span class="font-medium">16-03-2025</span></p>
              <p>Phương thức thanh toán: <span class="font-medium">Thanh toán khi nhận hàng (COD)</span></p>
              <p>
                Trạng thái:
                <span class="inline-flex items-center rounded bg-green-100 px-2 py-1 text-xs font-semibold text-green-700">
                  {{ store.ghnOrderData.status }}
                </span>
              </p>
            </div>
          </div>

          <!-- Địa chỉ giao hàng -->
          <div class="rounded-lg bg-white p-4 shadow">
            <h2 class="mb-2 text-lg font-semibold text-gray-700">Shipping address</h2>
            <div class="text-sm text-gray-600 space-y-1">
              <p class="font-medium text-gray-800">{{ store.ghnOrderData.toName }}</p>
              <p>{{ store.ghnOrderData.toAddress }}</p>
              <p>Điện thoại: {{ store.ghnOrderData.toPhone }}</p>
            </div>
          </div>
        </div>

        <!-- Danh sách sản phẩm -->
        <div class="mb-6 rounded-lg bg-white p-4 shadow">
          <h2 class="mb-4 text-lg font-semibold text-gray-700">Products in the order</h2>
          <div class="space-y-4">
            <!-- Sản phẩm 1 -->
            <div v-for="item in store.ghnOrderData.items">
              <div>Content: {{ store.ghnOrderData.content }}</div>
              <div class="flex items-center">
                <div class="flex-1">
                  <h4 class="font-semibold text-gray-800">{{ item.name }}</h4>
                  <p class="text-sm text-gray-500">Code: {{ item.code }}</p>
                </div>
                <div class="text-right">
                  <p class="text-sm text-gray-500">Quantity: {{ item.quantity }}</p>
                  <p class="text-sm text-gray-500">Height: {{ item.height }}</p>
                  <p class="text-sm text-gray-500">Weight: {{ item.weight }}</p>
                  <p class="text-sm text-gray-500">Width: {{ item.width }}</p>
                  <p class="text-sm text-gray-500">Length: {{ item.length }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>
<script lang="ts" setup>
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useTOTrackingOrderStore } from '@PKG_SRC/stores/Modules/DashBoard/TOTrackingOrderStore'

  const store = useTOTrackingOrderStore()

  onMounted(async () => {
    await store.GetTrackingOrder()
  })
</script>
