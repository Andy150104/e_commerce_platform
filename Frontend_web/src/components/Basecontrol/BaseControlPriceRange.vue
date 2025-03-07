<template>
  <div class="card">
    <DataTable
      v-model:filters="filters"
      :value="items"
      class="my-custom-table"
      :paginator="true"
      :rows="pageSize"
      :rows-per-page-options="[5, 10, 15]"
      dataKey="id"
      filterDisplay="row"
      :loading="loading"
      :globalFilterFields="globalFilterFields"
      stripedRows
      showGridlines
    >
      <template #header>
        <div class="flex justify-between">
          <Button type="button" icon="pi pi-filter-slash" label="Clear" outlined @click="clearFilter" />
          <div class="p-inputgroup">
            <IconField>
              <InputIcon>
                <i class="pi pi-search" />
              </InputIcon>
              <InputText v-model="filters.global.value" placeholder="Keyword Search" />
            </IconField>
          </div>
        </div>
      </template>
      <template #empty> No items found. </template>
      <template #loading> Loading data. Please wait. </template>
      <Column
        v-for="(col, index) in columns"
        :key="index"
        :field="col.field"
        :header="col.header"
        :sortable="col.isSort"
        :filter="col.isFilter"
        :filterField="col.field"
        :showFilterMenu="true"
        style="min-width: 12rem"
      >
        <template #body="{ data }">
          {{ data[col.field] }}
        </template>
        <template v-if="col.isFilter" #filter="{ filterModel, filterCallback }">
          <MultiSelect
            v-if="col.filterType === 'multiSelect'"
            v-model="filterModel.value"
            @change="filterCallback()"
            :options="col.options"
            :showClear="true"
            placeholder="Any"
            style="min-width: 12rem"
            display="chip"
            :maxSelectedLabels="1"
            selectedItemsLabel="{0} items selected"
          />

          <!-- Mặc định dùng InputText -->
          <InputText v-else v-model="filterModel.value" type="text" @input="filterCallback()" :placeholder="`Search by ${col.header}`" />
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue'
  import DataTable from 'primevue/datatable'
  import Column from 'primevue/column'
  import Button from 'primevue/button'
  import InputText from 'primevue/inputtext'
  import MultiSelect from 'primevue/multiselect'
  import { FilterMatchMode } from '@primevue/core/api'
  import 'primeicons/primeicons.css'

  const props = withDefaults(
    defineProps<{
      items: any[]
      columns: any[]
      pageSize?: number
    }>(),
    {
      pageSize: 10,
    }
  )

  const loading = ref(false)

  const filters = ref<any>({
    global: { value: null, matchMode: FilterMatchMode.CONTAINS },
  })

  props.columns.forEach((col) => {
    if (col.isFilter) {
      filters.value[col.field] = {
        value: null,
        matchMode: col.filterType === 'multiSelect' ? FilterMatchMode.IN : FilterMatchMode.CONTAINS,
      }
    }
  })
  const globalFilterFields = props.columns.map((col) => col.field)

  const clearFilter = (): void => {
    filters.value.global.value = null

    props.columns.forEach((col) => {
      if (col.isFilter && filters.value[col.field]) {
        filters.value[col.field].value = null
      }
    })
  }
</script>
<style>
  .my-custom-table .p-datatable-thead > tr > th {
    background-color: #e6e8e8;
    color: #000000;
  }

  .my-custom-table .p-datatable-thead > tr > th:hover {
    background-color: #dedede !important;
    color: #333 !important;
  }

  .my-custom-table .p-datatable-thead > tr.p-column-filter-row > th:hover {
    background-color: #f7bebe !important;
    color: #333 !important;
  }

  /* Dark Mode */
  .dark .my-custom-table .p-datatable-thead > tr > th {
    background-color: #515456;
    color: #ffffff;
  }

  .dark .my-custom-table .p-datatable-thead > tr > th:hover {
    background-color: #494949 !important;
    color: #fff !important;
  }
  .dark .my-custom-table .p-datatable-thead > tr.p-column-filter-row > th:hover {
    background-color: #666 !important;
    color: #fff !important;
  }
</style>
