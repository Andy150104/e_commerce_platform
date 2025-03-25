<template>
  <BaseScreenDashBoard>
    <template #body>
      <div>
        <h1 class="animate-fade-down animate-duration-1000 animate-delay-300 mb-6 text-2xl font-bold text-gray-800">My request</h1>
        <div class="animate-fade-up animate-duration-1000 animate-delay-300 mx-auto p-4 sm:p-6 lg:p-8 max-h-[80vh] 3xl:mx-32 overflow-y-auto">
          <BaseDataTable
            :items="store.ItemsList"
            :columns="columns"
            :is-show-add-button="false"
            :is-show-delete="false"
            :is-show-update="false"
            :is-selected-columns="true"
            @on-binding-insert-data=""
            @on-binding-update-data=""
            @on-update=""
            :is-status="true"
            :default-fields-select="['refundId', 'userName', 'refundAmount', 'refundStatus']"
          >
          </BaseDataTable>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>
<script setup lang="ts">
  import BaseDataTable from '@PKG_SRC/components/Table/BaseDataTable.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useCRCustomerRefundStore } from '@PKG_SRC/stores/Modules/DashBoard/CRCustomerRefundStore'
  import { useDHOHistoryStore } from '@PKG_SRC/stores/Modules/DashBoard/DHOHistoryStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const store = useCRCustomerRefundStore()
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
  const columns = [
    {
      field: 'refundId',
      header: 'Refund Id',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'userName',
      header: 'User Name',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'orderId',
      header: 'Order Id',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'refundAmount',
      header: 'Refund Amount',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'createdAt',
      header: 'Create dAt',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
  ]
  onMounted(async () => {
    await store.GetDashHistoryOrder()
  })
</script>
