<template>
  <BaseScreenProduct>
    <template #body>
      <!-- Thanh header -->
      <div class="w-full max-w-4xl mx-auto flex items-center px-4 py-3 shadow-md border-b bg-white dark:bg-gray-900 text-black dark:text-white">
        <button @click="goBack" class="text-xl mr-3">&#x2190;</button>
        <h2 v-if="cartStore.isPaymentFlag" class="text-lg font-semibold flex-1 text-center">Thông tin và Thanh toán</h2>
        <h2 v-else class="text-lg font-semibold flex-1 text-center">Giỏ hàng của bạn</h2>
      </div>

      <!-- Khi đang ở trạng thái thanh toán -->
      <div
        v-if="cartStore.isPaymentFlag"
        class="min-h-screen w-full flex flex-col items-center bg-white dark:bg-gray-900 text-black dark:text-white px-4 pt-6 pb-6"
      >
        <ModalInputForm
          :header="'Add Address'"
          :button-icon="'pi pi-plus'"
          :button-label="'Add your address'"
          :button-severity="'contrast'"
          :confirm-label="'Ok'"
          :width="'40rem'"
        >
          <template #body>
            <div class="max-w-4xl w-full h-[850px] bg-gray-100 dark:bg-gray-800 p-6 rounded-lg shadow-lg overflow-y-auto space-y-4">
              <div
                v-for="(address, index) in addressStore.uUDSSelectUserAddressEntity ?? []"
                :key="index"
                :class="[
                  'flex items-center justify-between p-4 bg-white dark:bg-gray-700 rounded-lg shadow',
                  store.address === address ? 'border-2 border-blue-500' : '',
                ]"
              >
                <!-- Thông tin địa chỉ -->
                <div class="flex-1">
                  <div class="text-gray-700 dark:text-gray-300 font-bold text-xl">
                    {{ (address?.firstName ?? '') + ' ' + (address?.lastName ?? '') }}
                  </div>
                  <p class="text-gray-700 dark:text-gray-300">{{ address.addressLine }}</p>
                  <p class="text-gray-700 dark:text-gray-300">{{ address.ward }}</p>
                  <p class="text-gray-700 dark:text-gray-300">{{ address.city }}, {{ address.province }} {{ address.district }}</p>
                </div>
                <!-- Nút chọn địa chỉ -->
                <div>
                  <button class="bg-blue-500 text-white px-4 py-2 rounded" @click="selectAddress(address)">Chọn</button>
                </div>
              </div>
            </div>
          </template>
        </ModalInputForm>
        <div v-if="store.address" :class="['flex items-center justify-between p-4 bg-white dark:bg-gray-700 rounded-lg shadow']">
          <!-- Thông tin địa chỉ -->
          <div class="flex-1">
            <div class="text-gray-700 dark:text-gray-300 font-bold text-xl">
              {{ (store.address?.firstName ?? '') + ' ' + (store.address?.lastName ?? '') }}
            </div>
            <p class="text-gray-700 dark:text-gray-300">{{ store.address.addressLine }}</p>
            <p class="text-gray-700 dark:text-gray-300">{{ store.address.ward }}</p>
            <p class="text-gray-700 dark:text-gray-300">{{ store.address.city }}, {{ store.address.province }} {{ store.address.district }}</p>
          </div>
        </div>
        <div v-else></div>
        <div class="max-w-4xl w-full mt-12 flex items-center justify-between p-4 rounded-lg bg-gray-100 dark:bg-gray-700 text-black dark:text-white">
          <!-- Nút thanh toán -->
          <button class="bg-blue-500 px-6 py-3 rounded-lg font-semibold" @click="onCreateOrderAndPay">Thanh toán</button>
        </div>
      </div>

      <!-- Khi không ở trạng thái thanh toán (giỏ hàng) -->
      <div v-else class="min-h-screen w-full flex flex-col items-center bg-white dark:bg-gray-900 text-black dark:text-white px-4 pt-6 pb-6">
        <div class="max-w-4xl w-full h-[850px] bg-gray-100 dark:bg-gray-800 p-6 rounded-lg shadow-lg overflow-y-auto">
          <CardCart :cart-model="cartStore.cartList" />
        </div>
        <div class="max-w-4xl w-full mt-12 flex items-center justify-between p-4 rounded-lg bg-gray-100 dark:bg-gray-700 text-black dark:text-white">
          <!-- Text hiển thị tổng tiền -->
          <p class="font-semibold">Tổng: {{ moneyFormatter(Number(cartStore.totalCartPrice)) }} VND</p>
          <!-- Nút thanh toán -->
          <button class="bg-blue-500 px-6 py-3 rounded-lg font-semibold" @click="onPayment">Thanh toán</button>
        </div>
      </div>
    </template>
  </BaseScreenProduct>
</template>

<script setup lang="ts">
  import { ref } from 'vue'
  import CardCart from '@PKG_SRC/components/Card/CardCart.vue'
  import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue'
  import BaseScreenProduct from '@PKG_SRC/layouts/Basecreen/BaseScreenProduct.vue'
  import { useCartStore } from '@PKG_SRC/stores/Modules/Blind_Box/CartStore'
  import { useDashAddressStore } from '@PKG_SRC/stores/Modules/DashBoard/dashboardAddressStore'
  import { Currency, Locale } from '@PKG_SRC/types/enums/constantFrontend'
  import { useProfilePaymentStore } from '@PKG_SRC/stores/Modules/Service/ProfilePaymentStore'

  const cartStore = useCartStore()
  const addressStore = useDashAddressStore()
  const store = useProfilePaymentStore()

  const selectedAddress = ref(null)

  const moneyFormatter = (money?: number) => {
    if (money) return formatMoney(money, Currency.VND, Locale.VI_VN)
  }

  const selectAddress = (address: any) => {
    store.address = address
    console.log('Địa chỉ được chọn:', store.address)
  }

  const onCreateOrderAndPay = async () => {
    if ((await store.InsertOrder())){
      window.location.href = store.paymentUrl
    }
  }

  const onPayment = async () => {
    cartStore.isPaymentFlag = true
    await addressStore.GetAddress()
  }

  // Quay lại trang trước
  const goBack = () => {
    if (cartStore.isPaymentFlag) {
      cartStore.isPaymentFlag = false
      return
    }
    window.history.back()
  }
</script>
