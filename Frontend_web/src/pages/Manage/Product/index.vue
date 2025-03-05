<template>
  <BaseScreenManage>
    <template #body>
      <div
        class="animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"
      >
        <BaseDataTable :items="store.accessoriesList" :columns="columns" :is-selected-columns="true"/>
      </div>
    </template>
  </BaseScreenManage>
</template>
<script lang="ts" setup>
  import BaseDataTable from '@PKG_SRC/components/Table/BaseDataTable.vue'
  import BaseScreenManage from '@PKG_SRC/layouts/Basecreen/BaseScreenManage.vue'
  import { useMPSProductStore } from '@PKG_SRC/stores/Modules/MPS/MPSProductStore'

  const store = useMPSProductStore()

  const columns = [
    {
      field: 'accessoryId',
      header: 'Accessory Id',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'accessoryName',
      header: 'Accessory Name',
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
      field: 'price',
      header: 'Price',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'discount',
      header: 'Discount',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'quantity',
      header: 'Quantity',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'totalReviews',
      header: 'Total Reviews',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
    {
      field: 'totalSold',
      header: 'Total Sold',
      isFilter: true,
      isSort: true,
      filterType: 'text',
    },
  ]

  onMounted(async () => {
    await store.GetAllProduct()
  })
</script>
