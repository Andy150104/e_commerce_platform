<template>
  <div v-if="isSuccess" class="relative flex min-h-screen items-center justify-center bg-white">
    <div class="pointer-events-none absolute inset-0 overflow-hidden" aria-hidden="true">
      <!-- Shape 1 -->
      <div class="absolute top-[10%] left-[20%] h-6 w-3 rotate-12 transform animate-bounce rounded-sm bg-pink-400"></div>
      <!-- Shape 2 -->
      <div class="absolute top-[25%] left-[50%] h-3 w-3 rotate-45 transform animate-ping rounded-full bg-green-400"></div>
      <!-- Shape 3 -->
      <div class="absolute top-[40%] left-[70%] h-5 w-2 rotate-[30deg] transform animate-bounce rounded-sm bg-yellow-400"></div>
      <!-- Shape 4 -->
      <div class="absolute top-[60%] left-[15%] h-4 w-4 rotate-12 transform animate-ping rounded-full bg-blue-400"></div>
      <!-- Shape 5 -->
      <div class="absolute top-[75%] left-[45%] h-4 w-2 rotate-[60deg] transform animate-bounce bg-indigo-400"></div>
      <!-- Shape 6 -->
      <div class="absolute top-[85%] left-[80%] h-3 w-3 rotate-45 transform animate-ping rounded-full bg-red-400"></div>
    </div>
    <div class="relative z-10 mx-auto flex max-w-md flex-col items-center rounded-xl bg-white p-6 shadow-xl md:p-8">
      <!-- Icon check -->
      <div class="mb-4 flex h-16 w-16 items-center justify-center rounded-full bg-green-100">
        <svg class="h-8 w-8 text-green-600" fill="none" stroke="currentColor" stroke-width="3" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" d="M5 13l4 4L19 7" />
        </svg>
      </div>
      <h1 class="mb-2 text-2xl font-bold text-gray-800">Payment succeeded!</h1>
      <!-- Nội dung mô tả -->
      <p class="mb-4 text-center text-gray-600">
        Thank you for processing your most recent payment.<br />
        Your premium subscription will expire on <strong>June 2, 2024</strong>.
      </p>
      <!-- Nút hành động -->
      <router-link to="/Dashboard/UserProfile" class="inline-block rounded-md bg-green-600 px-5 py-2 font-semibold text-white transition-colors hover:bg-green-500">
        Your dashboard
      </router-link>
    </div>
  </div>
</template>
<script lang="ts" setup>
  import { useTransactionStore } from '@PKG_SRC/stores/Modules/Transaction/transactionStore'
  import { ref, computed, onMounted } from 'vue'
  import { useRoute } from 'vue-router'

  const store = useTransactionStore()
  const successValue = ref<boolean>(false)
  const isSuccess = computed(() => successValue.value)

  onMounted(() => {
    const route = useRoute()
    const successParam = route.query.success ?? ''
    const orderIdParam = route.query.orderId ?? ''
    const ghnOrderCodeParam = route.query.GhnOrderCode ?? ''
    successValue.value = successParam === 'true'
    store.orderId = typeof orderIdParam === 'string' ? orderIdParam : ''
    store.GhnOrderCode = typeof ghnOrderCodeParam === 'string' ? ghnOrderCodeParam : ''
    store.updateOrder()
  })
</script>
