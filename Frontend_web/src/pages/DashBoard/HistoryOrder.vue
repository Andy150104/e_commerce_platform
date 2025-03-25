<template>
  <BaseScreenDashBoard>
    <template #body>
      <div>
        <h1 class="animate-fade-down animate-duration-1000 animate-delay-300 mb-6 text-2xl font-bold text-gray-800">History Order</h1>
        <div class="animate-fade-up animate-duration-1000 animate-delay-300 mx-auto p-4 sm:p-6 lg:p-8 max-h-[80vh] 3xl:mx-32 overflow-y-auto">
          <div
            v-for="item in store.DOHSelectHistoryOrder"
            class="space-y-6 mb-12 animate-fade-right animate-once animate-duration-1000 animate-delay-[600ms] animate-ease-in-out"
          >
            <!-- Bắt đầu 1 Đơn hàng -->
            <div class="rounded-lg border border-gray-200 bg-white p-6 shadow-md">
              <!-- Header đơn hàng: Mã đơn, Ngày đặt, Trạng thái, v.v. -->
              <div class="mb-4 flex flex-col border-b pb-4 sm:flex-row sm:items-center sm:justify-between">
                <div class="flex flex-col space-y-1 text-sm text-gray-600">
                  <span class="font-semibold text-gray-800">Mã đơn hàng: {{ item.orderId }}</span>
                  <span>Ngày đặt: {{ item.createdAt }}</span>
                  <span
                    >Trạng thái:
                    <span class="font-semibold text-green-600">Đã hoàn thành</span>
                  </span>
                </div>
                <!-- Nút Xem chi tiết (tuỳ chọn) -->
                <div class="mt-3 sm:mt-0">
                  <button class="rounded-md bg-blue-600 px-4 py-2 text-sm text-white transition hover:bg-blue-700">Xem chi tiết</button>
                </div>
              </div>

              <!-- Danh sách sản phẩm -->
              <div v-for="product in item.orderDetails" class="space-y-4">
                <!-- Sản phẩm 1 -->
                <div class="flex items-center">
                  <div class="flex-1">
                    <h4 class="font-semibold text-gray-800">{{ product.accessoryName }}</h4>
                    <p class="text-sm text-gray-500">Accessory Id: {{ product.accessoryId }}</p>
                  </div>
                  <div class="text-right">
                    <p class="font-semibold text-gray-800">{{ product.originalPrice }}</p>
                    <p class="text-sm text-gray-500">Quantity: {{ product.quantity }}</p>
                  </div>
                </div>
              </div>

              <!-- Footer đơn hàng: Tổng tiền, nút action -->
              <div class="mt-4 flex flex-col border-t pt-4 sm:flex-row sm:items-center sm:justify-between">
                <!-- Tổng tiền -->
                <div class="mb-4 sm:mb-0">
                  <span class="mr-2 text-lg font-semibold text-gray-700">Total Order:</span>
                  <span class="text-xl font-bold text-red-600">{{ item.totalPrice }}</span>
                </div>
                <!-- Nút action -->
                <div class="space-x-2">
                  <button class="rounded-md border border-gray-300 px-4 py-2 text-sm text-gray-700 transition hover:bg-gray-100">Xem Shop</button>
                  <ModalInputForm
                    :header="'Return/Refund Request'"
                    :button-icon="''"
                    :button-label="'Send Refund Request'"
                    :button-severity="'contrast'"
                    :width="'80rem'"
                    @on-confirm="onDoRefund"
                    @on-blinding-insert="onBindlingData(item.orderId ?? '')"
                  >
                    <template #body>
                      <BaseControlEditorInput :xml-column="xmlColumns.refundReason" v-model="editorValue" />
                    </template>
                  </ModalInputForm>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>
<script setup lang="ts">
  import BaseControlEditorInput from '@PKG_SRC/components/Basecontrol/BaseControlEditorInput.vue'
  import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useDHOHistoryStore } from '@PKG_SRC/stores/Modules/DashBoard/DHOHistoryStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const store = useDHOHistoryStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const editorValue = ref('')
  const orderId = ref<string>('')
  const xmlColumns = {
    refundReason: XmlLoadColumn({
      id: 'refundReason',
      name: 'Refund Reason',
      rules: 'required',
      visible: true,
      option: '',
    }),
  }

  const onBindlingData = (id :string) => {
    orderId.value =  id
  }

  const onDoRefund = async () => {
    await store.RefundMoney(orderId.value, editorValue.value)
  }

  onMounted(async () => {
    await store.GetDashHistoryOrder()
  })
</script>
