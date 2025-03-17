<template>
  <BaseScreenManage>
    <template #body>
        <div class="max-w-3xl mx-auto text-center relative z-10 mt-0 pt-0">
          <h1 class="text-4xl font-bold text-black dark:text-white mb-4 animate-fade-up animate-duration-1000 animate-delay-500">Manage Exchange Requests</h1>
          <p class="text-lg text-gray-700 mb-8 animate-fade-up animate-duration-1000 animate-delay-500">Manage Exchange Recheck Request!</p>
          <div class="mt-8 flex justify-center space-x-4"></div>
        </div>
      <div
        class="animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"
      >
      <BaseDataTable
  :items="store.uAEPSGetExchangeRecheckRequestAccessoryEntity"
  :columns="columns"
  :is-selected-columns="true"
  @on-binding-update-data="onBinding"
  @on-toggle-status="handleToggleStatus"
  @on-accept="handleAccept"
  @on-unaccept="handleUnAccept"
  :default-fields-select="['requestId', 'status']"
   :showActions="false"
/>
      </div>
    </template>
  </BaseScreenManage>
</template>
<script lang="ts" setup>
  import BaseDataTable from '@PKG_SRC/components/Table/BaseDataTable.vue'
  import BaseScreenManage from '@PKG_SRC/layouts/Basecreen/BaseScreenManage.vue'
  import { useDBExchangeStore } from '@PKG_SRC/stores/Modules/DashBoard/DBExchangeStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const store = useDBExchangeStore()
  const editorValue = ref('')
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
    requestId: XmlLoadColumn({
      id: 'requestId',
      name: 'Request Id',
      rules: 'required',
      visible: true,
      option: '',
    }),
    exchangeId: XmlLoadColumn({
      id: 'exchangeId',
      name: 'Exchange Id',
      rules: 'required',
      visible: true,
      option: '',
    }),
    createdAt: XmlLoadColumn({
      id: 'createdAt',
      name: 'Created At',
      rules: 'required',
      visible: true,
      option: '',
    }),
    createdBy: XmlLoadColumn({
      id: 'createdBy',
      name: 'Created By',
      rules: 'required',
      visible: true,
      option: '',
    }),
    description: XmlLoadColumn({
      id: 'description',
      name: 'Description',
      rules: 'required',
      visible: true,
      option: '',
    }),
    updatedBy: XmlLoadColumn({
      id: 'updatedBy',
      name: 'Updated By',
      rules: 'required',
      visible: true,
      option: '',
    }),
    updatedAt: XmlLoadColumn({
      id: 'updatedAt',
      name: 'Updated At',
      rules: '',
      visible: true,
      option: '',
    }),
    
  }
  const columns = [
    {
      field: 'requestId',
      header: 'Request Id',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'exchangeId',
      header: 'Exchange Id',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'description',
      header: 'Description',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'createdAt',
      header: 'Created At',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'createdBy',
      header: 'Created By',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'updatedBy',
      header: 'Updated By',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'updatedAt',
      header: 'Updated At',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'status',
      header: 'Status',
      isFilter: true,
      isSort: true,
      filterType: 'byte',
    },
    {
      field: 'isActive',
      header: 'Is Active',
      isFilter: true,
      isSort: true,
      filterType: 'boolean',
    },
    

 ]

  const onBinding = async (items: any) => {
    await nextTick()
    store.fields.setFieldValue('requestId', items.requestId)
    store.fields.setFieldValue('exchangeId', items.exchangeId)
    store.fields.setFieldValue('status', items.status)
    store.fields.setFieldValue('isActive', items.isActive)
    store.fields.setFieldValue('createdAt', items.createdAt)
    store.fields.setFieldValue('createdBy', items.createdBy)
    store.fields.setFieldValue('updatedBy', items.updatedBy)
    store.fields.setFieldValue('updatedAt', items.updatedAt)
    editorValue.value = items.description
  }
  const handleToggleStatus = async (item: any) => {
  try {
    console.log("Item nhận được:", item);
    const { requestId, isAccepted } = item;

    console.log("requestId:", requestId);
    console.log("isAccepted:", isAccepted);

    if (isAccepted) {
      await handleAccept(requestId);
    } else {
      await handleUnAccept(requestId);
    }
  } catch (error) {
    console.error('Error toggling request status:', error);
  }
};


  const handleAccept = async (requestId: string) => {
  try {
    await store.ManageExchange(requestId,true)
    await store.GetExchange() // Reload data
  } catch (error) {
    console.error('Error accepting request:', error)
  }
}

const handleUnAccept = async (requestId: string) => {
  try {
    await store.ManageExchange(requestId,false)
    await store.GetExchange() // Reload data
  } catch (error) {
    console.error('Error unaccepting request:', error)
  }
}

  onMounted(async () => {
    await store.GetExchange()
  })
</script>
<style>
  h1 {
    font-size: 2rem !important;
    font-weight: bold;
  }

  h2 {
    font-size: 1.75rem !important;
  }

  h3 {
    font-size: 1.5rem !important;
  }
</style>
