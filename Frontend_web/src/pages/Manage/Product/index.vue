<template>
  <BaseScreenManage>
    <template #body>
      <div
        class="animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"
      >
        <BaseDataTable
          :items="store.accessoriesList"
          :columns="columns"
          :is-selected-columns="true"
          @on-delete="onDelete"
          @on-insert="onInsert"
          :default-fields-select="['accessoryId', 'accessoryName', 'price', 'imageAccessoriesList']"
        >
          <template #bodyButtonAdd>
            <div class="grid grid-cols-2 gap-6 mb-8">
              <div>
                <UserControlTextFieldFloatLabel :xml-column="xmlColumns.name" :maxlength="50" :disabled="false" :err-msg="fieldErrors.name" />
                <UserControlTextFieldFloatLabel
                  :xml-column="xmlColumns.quantity"
                  :maxlength="50"
                  :disabled="false"
                  :err-msg="fieldErrors.quantity"
                  :is-numberic="true"
                  :is-not-phone-number="true"
                />
              </div>
              <div>
                <UserControlTextFieldFloatLabel
                  :xml-column="xmlColumns.price"
                  :maxlength="50"
                  :disabled="false"
                  :err-msg="fieldErrors.price"
                  :is-numberic="true"
                  :is-not-phone-number="true"
                />
                <UserControlTextFieldFloatLabel
                  :xml-column="xmlColumns.discount"
                  :maxlength="50"
                  :disabled="false"
                  :err-msg="fieldErrors.discount"
                  :is-numberic="true"
                  :is-not-phone-number="true"
                />
              </div>
            </div>
            <UserControlUploadImage :max-number-image="5" :is-show-popover="true" :label="'Upload avatar image'" />
            <BaseControlEditorInput :xml-column="xmlColumns.description" v-model="editorValue" />
            <UserControlTextFieldFloatLabel
              :xml-column="xmlColumns.shortDescription"
              :maxlength="50"
              :disabled="false"
              :err-msg="fieldErrors.shortDescription"
            />
          </template>
        </BaseDataTable>
      </div>
    </template>
  </BaseScreenManage>
</template>
<script lang="ts" setup>
  import BaseControlEditorInput from '@PKG_SRC/components/Basecontrol/BaseControlEditorInput.vue'
  import BaseDataTable from '@PKG_SRC/components/Table/BaseDataTable.vue'
  import UserControlTextFieldFloatLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldFloatLabel.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import BaseScreenManage from '@PKG_SRC/layouts/Basecreen/BaseScreenManage.vue'
  import { useMPSProductStore } from '@PKG_SRC/stores/Modules/MPS/MPSProductStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const store = useMPSProductStore()
  const editorValue = ref('')
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
    quantity: XmlLoadColumn({
      id: 'quantity',
      name: 'Quantity',
      rules: 'required',
      visible: true,
      option: '',
    }),
    name: XmlLoadColumn({
      id: 'name',
      name: 'Product Name',
      rules: 'required',
      visible: true,
      option: '',
    }),
    price: XmlLoadColumn({
      id: 'price',
      name: 'Product Price',
      rules: 'required|min_max:0,9999999999',
      visible: true,
      option: '',
    }),
    discount: XmlLoadColumn({
      id: 'discount',
      name: 'Discount',
      rules: 'required|min_max:0,100',
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
    shortDescription: XmlLoadColumn({
      id: 'shortDescription',
      name: 'Short Description',
      rules: 'required',
      visible: true,
      option: '',
    }),
  }
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
    {
      field: 'imageAccessoriesList',
      header: 'Images',
      isFilter: false,
      isSort: false,
      filterType: '',
    },
  ]
  const onDelete = async (items: any) => {
    await store.DeleteOneProduct(items.accessoryId)
    await store.GetAllProduct()
  }

  const onInsert = async () => {
    await store.AddProduct(editorValue.value)
    await store.GetAllProduct()
  }

  onMounted(async () => {
    await store.GetAllProduct()
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
