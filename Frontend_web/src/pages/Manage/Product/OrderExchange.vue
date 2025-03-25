<template>
<BaseScreenDashBoard>
    <template #body>
      <div class="max-w-3xl mx-auto text-center relative z-10 mt-0 pt-0">
        <h1 class="text-center font-extrabold text-3xl dark:text-white">Manage Exchange Requests</h1>
        <p class="text-lg text-gray-700 mb-8 animate-fade-up animate-duration-1000 animate-delay-500">View Exchange History!</p>
        <div class="mt-8 flex justify-center space-x-4"></div>
      </div>
      <div
        class="animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"
      >
        <BaseDataTable
          :items="store.OrderHistoryList"
          :columns="columns"
          :is-selected-columns="true"
          @on-binding-update-data="onBinding"
          :default-fields-select="['exchangeId','exchangeName']"
          :showActions="false"
        />
      </div>
    </template>
</BaseScreenDashBoard>
</template>

<script setup lang="ts">
  import BaseDataTable from '@PKG_SRC/components/Table/BaseDataTable.vue';
import type { VEXSGetOrderExchangeResponseEntity } from '@PKG_SRC/composables/Client/api/@types';
import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
import { useOrderEXHSStore } from '@PKG_SRC/stores/Modules/DashBoard/OrderEX';

const store = useOrderEXHSStore()
const columns = [
    {
      field: 'exchangeId',
      header: 'Exchange Id',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'exchangeName',
      header: 'Exchange Name',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'created_at',
      header: 'created At',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'created_by',
      header: 'created By',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    }, 
    {
      field: 'updated_at',
      header: 'Updated At',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'updated_by',
      header: 'Updated By',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
  ]
  const formattedItems = computed(() => {
    return store.OrderHistoryList
  })
  const onBinding = async (items: VEXSGetOrderExchangeResponseEntity) => {
    await nextTick()
    store.fields.setFieldValue('exchangeId', items.exchangeId)
    store.fields.setFieldValue('exchangeName', items.exchangeName)
  }
  onMounted(async () => {
    await store.GetOrderHistory()
  })
</script>