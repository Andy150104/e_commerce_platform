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
        <div class="flex flex-col gap-2">
          <div class="flex justify-between">
            <div class="flex justify-between gap-6">
              <Button type="button" icon="pi pi-filter-slash" label="Clear" outlined @click="clearFilter" />
              <ModalInputForm
                :header="'Add New Product'"
                :button-icon="'pi pi-plus'"
                :button-label="'New Product'"
                :button-severity="'contrast'"
                :width="'80rem'"
                @on-confirm="emit('on-insert')"
              >
                <template #body>
                  <slot name="bodyButtonAdd"></slot>
                </template>
              </ModalInputForm>
            </div>
            <div class="p-inputgroup">
              <IconField>
                <InputIcon>
                  <i class="pi pi-search" />
                </InputIcon>
                <InputText v-model="filters.global.value" placeholder="Keyword Search" />
              </IconField>
            </div>
          </div>
          <!-- Hiển thị MultiSelect nếu isSelectedColumns bật -->
          <div v-if="isSelectedColumns">
            <MultiSelect
              v-model="selectedColumns"
              :options="columns"
              optionLabel="header"
              @update:modelValue="onToggle"
              display="chip"
              placeholder="Select Columns"
            />
          </div>
        </div>
      </template>
      <template #empty> No items found. </template>
      <template #loading> Loading data. Please wait. </template>
      <Column
        v-for="(col, index) in displayedColumns"
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
          <template v-if="col.field === 'imageAccessoriesList'">
            <GalleryCarouselPopup :images="data[col.field]" />
          </template>
          <template v-else>
            {{ data[col.field] }}
          </template>
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
      <Column header="Actions" style="min-width: 12rem">
        <template #body="slotProps">
          <ModalInputForm
            :is-only-show-icon="true"
            :header="'Update Accessory With Id <strong>' + slotProps.data.accessoryId + '</strong>'"
            :button-icon="'pi pi-pencil'"
            :button-severity="'success'"
            :width="'80rem'"
            @on-confirm="onUpdateAccessory(slotProps.data)"
          >
            <template #body>
              <slot name="bodyButtonAdd"></slot>
            </template>
          </ModalInputForm>
          <ModalConfirm
            :is-only-show-icon="true"
            @on-confirm="confirmDeleteProduct(slotProps.data)"
            :content="'Are you sure you want to delete <strong>' + slotProps.data.accessoryId + '</strong>?'"
          />
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script setup lang="ts">
  import { ref, computed } from 'vue'
  import DataTable from 'primevue/datatable'
  import Column from 'primevue/column'
  import Button from 'primevue/button'
  import InputText from 'primevue/inputtext'
  import MultiSelect from 'primevue/multiselect'
  import { FilterMatchMode } from '@primevue/core/api'
  import 'primeicons/primeicons.css'
  import ModalConfirm from '../Modal/ModalConfirm.vue'
  import ModalInputForm from '../Modal/ModalInputForm.vue'
  import GalleryCarouselPopup from '../Gallery/GalleryCarouselPopup.vue'
  import { data } from 'autoprefixer'

  // Định nghĩa props với prop isSelectedColumns (mặc định là false)
  const props = withDefaults(
    defineProps<{
      items: any[]
      defaultFieldsSelect: string[]
      columns: any[]
      pageSize?: number
      isSelectedColumns?: boolean
    }>(),
    {
      pageSize: 10,
      isSelectedColumns: false,
    }
  )

  const loading = ref(false)

  const filters = ref<any>({
    global: { value: null, matchMode: FilterMatchMode.CONTAINS },
  })

  interface Emits {
    (e: 'on-delete', item: any): void
    (e: 'on-update', item: any): void
    (e: 'on-insert'): void
  }
  const emit = defineEmits<Emits>()

  const editProduct = (product: any) => {
    emit('on-update', product)
  }

  const confirmDeleteProduct = (product: any) => {
    emit('on-delete', product)
  }

  const onUpdateAccessory = (product: any) => {
    emit('on-update', product)
  }

  // Thiết lập filter cho từng cột nếu cột có isFilter
  props.columns.forEach((col) => {
    if (col.isFilter) {
      filters.value[col.field] = {
        value: null,
        matchMode: col.filterType === 'multiSelect' ? FilterMatchMode.IN : FilterMatchMode.CONTAINS,
      }
    }
  })
  const globalFilterFields = props.columns.map((col) => col.field)

  // Hàm clear filter cho global và các cột riêng
  const clearFilter = (): void => {
    filters.value.global.value = null
    props.columns.forEach((col) => {
      if (col.isFilter && filters.value[col.field]) {
        filters.value[col.field].value = null
      }
    })
  }

  const selectedColumns = ref(props.columns.filter((col) => props.defaultFieldsSelect.includes(col.field)))

  const onToggle = (val: any) => {
    selectedColumns.value = props.columns.filter((col) => val.some((v: any) => v.field === col.field))
  }

  const displayedColumns = computed(() => {
    return props.isSelectedColumns ? selectedColumns.value : props.columns
  })
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
