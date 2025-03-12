<template>
  <BaseScreenDashBoard>
    <template #body>
      <div class="container mx-auto px-4 py-8" :class="{ dark: isDarkMode }">
        <div class="flex justify-between items-center mr-5 ml-6 mb-6">
          <h1 class="text-2xl font-bold dark:text-white">My Addresses</h1>
          <button @click="addNewAddress" class="bg-blue-500 hover:bg-blue-600 text-white font-semibold py-2 px-4 rounded flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" class="w-5 h-5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M12 4.5v15m7.5-7.5h-15" />
            </svg>
            Add New Address
          </button>
        </div>

        <div class="bg-white dark:bg-gray-800 shadow-md rounded-lg p-6">
          <div v-for="(address, index) in store.uUDSSelectUserAddressEntity ?? []" :key="index">
            <div class="flex flex-col sm:flex-row justify-between items-center pb-4">
              <div class="text-left">
                <div class="text-gray-700 dark:text-gray-300 font-bold flex items-center text-xl gap-2">
                  {{ (address?.firstName ?? '') + ' ' + (address?.lastName ?? '') }}
                </div>
                <p class="text-gray-700 dark:text-gray-300">{{ address.ward }}</p>
                <p class="text-gray-700 dark:text-gray-300">
                  {{
                    `${address.city}, ${address.province}
                  ${address.district}`
                  }}
                </p>
              </div>
              <div class="mt-4 flex flex-col items-end space-y-2">
                <h2
                  @click="updateAddress(address.addressId ?? '')"
                  class="cursor-pointer text-blue-500 hover:text-blue-600 font-semibold dark:text-blue-400 dark:hover:text-blue-300"
                >
                  Update Address
                </h2>
                <button
                  @click="openConfirmDelete(address.addressId ?? '')"
                  class="bg-red-500 hover:bg-red-600 text-white font-semibold py-2 px-4 rounded"
                >
                  Remove
                </button>
              </div>
            </div>
            <hr v-if="index < store.uUDSSelectUserAddressEntity.length - 1" class="border-gray-300 dark:border-gray-600 my-4" />
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>

  <!-- Modal Add/Edit Address -->
  <BModal v-model="isAddressModalOpen" :title="isEditing ? 'Edit Address' : 'Add New Address'" labelModelName="AddressModal">
    <template #default>
      <div class="space-y-4">
        <div>
          <UserControlTextFieldLabel
            :xml-column="xmlColumns.addressLine"
            :maxlength="50"
            :disabled="false"
            :err-msg="fieldErrors.addressLine"
            :placeholder="'Enter your street address'"
          />
        </div>
        <div>
          <UserControlTextFieldLabel
            :xml-column="xmlColumns.ward"
            :maxlength="50"
            :disabled="false"
            :err-msg="fieldErrors.ward"
            :placeholder="'Enter your ward'"
          />
        </div>
        <div>
          <UserControlTextFieldLabel
            :xml-column="xmlColumns.city"
            :maxlength="50"
            :disabled="false"
            :err-msg="fieldErrors.city"
            :placeholder="'Enter your city'"
          />
        </div>
        <div>
          <UserControlTextFieldLabel
            :xml-column="xmlColumns.province"
            :maxlength="50"
            :disabled="false"
            :err-msg="fieldErrors.province"
            :placeholder="'Enter your province'"
          />
        </div>
        <div>
          <UserControlTextFieldLabel
            :xml-column="xmlColumns.district"
            :maxlength="50"
            :disabled="false"
            :err-msg="fieldErrors.district"
            :placeholder="'Enter your district'"
          />
        </div>
      </div>
    </template>
    <template #footer>
      <div class="flex justify-between w-full">
        <UButton @click="isAddressModalOpen = false">Cancel</UButton>
        <UButton color="blue" @click="saveAddress">Save</UButton>
      </div>
    </template>
  </BModal>
  <!-- Modal Confirm Delete -->
  <BModal v-model="isConfirmDeleteOpen" title="Confirm Remove" labelModelName="deleteModal">
    <p>Bạn có chắc chắn muốn xóa địa chỉ này không?</p>
    <template #footer>
      <div class="flex justify-between w-full">
        <UButton @click="isConfirmDeleteOpen = false">Cancel</UButton>
        <UButton color="red" @click="removeAddress">Remove</UButton>
      </div>
    </template>
  </BModal>
</template>

<script lang="ts" setup>
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'
  import { storeToRefs } from 'pinia'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import { useDashAddressStore } from '@PKG_SRC/stores/Modules/DashBoard/dashboardAddressStore'
  import { ref, onMounted } from 'vue'
  import BModal from '@PKG_SRC/components/Modal/BModal.vue'

  const store = useDashAddressStore()
  const isConfirmDeleteOpen = ref(false)
  const addressToDelete = ref<string | null>(null)
  const isAddressModalOpen = ref(false)
  const isEditing = ref(false)
  const addressForm = ref({
    addressId: '',
    firstName: '',
    lastName: '',
    addressLine: '',
    ward: '',
    city: '',
    province: '',
    district: '',
  })

  const successMessage = ref(false)
  const { fieldValues, fieldErrors } = storeToRefs(store)
  console.log('fieldValues Initial:', fieldValues)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)

  const xmlColumns = {
    addressId: XmlLoadColumn({
      id: 'addressId',
      name: 'addressId',
      rules: 'required',
      visible: true,
      option: '',
    }),
    addressLine: XmlLoadColumn({
      id: 'addressLine',
      name: 'address Line',
      rules: 'required',
      visible: true,
      option: '',
    }),
    ward: XmlLoadColumn({
      id: 'ward',
      name: 'ward',
      rules: '',
      visible: true,
      option: '',
    }),
    city: XmlLoadColumn({
      id: 'city',
      name: 'city',
      rules: 'required',
      visible: true,
      option: '',
    }),
    province: XmlLoadColumn({
      id: 'province',
      name: 'province',
      rules: 'required',
      visible: true,
      option: '',
    }),
    district: XmlLoadColumn({
      id: 'district',
      name: 'district',
      rules: 'required',
      visible: true,
      option: '',
    }),
  }
  const addNewAddress = () => {
    isEditing.value = false
    store.ResetStore()

    isAddressModalOpen.value = true
  }

  const updateAddress = async (addressId: string) => {
    const address = await store.uUDSSelectUserAddressEntity.find((a) => a.addressId === addressId)
    if (!address) {
      console.error('Không tìm thấy địa chỉ với ID:', addressId)
      return
    }

    isEditing.value = true
    isAddressModalOpen.value = true
    store.fields.setFieldValue('addressId', address.addressId ?? '')
    store.fields.setFieldValue('addressLine', address.addressLine ?? '')
    store.fields.setFieldValue('city', address.city ?? '')
    formContext.setFieldValue('district', address.district ?? '')
    formContext.setFieldValue('ward', address.ward ?? '')
    formContext.setFieldValue('province', address.province ?? '')
  }

  const isSaving = ref(false)
  const saveAddress = async () => {
    if (isSaving.value) return
    isSaving.value = true

    // Đảm bảo không có undefined khi gán vào store
    store.SetFields({
      addressId: formContext.values.addressId ?? '',
      addressLine: formContext.values.addressLine ?? '',
      city: formContext.values.city ?? '',
      district: formContext.values.district ?? '',
      ward: formContext.values.ward ?? '',
      province: formContext.values.province ?? '',
    })

    console.log('SaveForm:', formContext.values)

    let success = false
    if (isEditing.value) {
      success = await store.UpdateAddress()
    } else {
      success = await store.InsertAddress()
    }

    if (success) {
      isAddressModalOpen.value = false
      store.GetAddress() // Load lại danh sách sau khi thêm/sửa
    }

    isSaving.value = false
  }

  const openConfirmDelete = (id: string) => {
    addressToDelete.value = id
    isConfirmDeleteOpen.value = true
  }

  const removeAddress = async () => {
    if (addressToDelete.value) {
      formContext.setFieldValue('addressId', addressToDelete.value)
      console.log('Store values:' + formContext.values.addressId)

      console.log('addressToDelete values:' + addressToDelete.value)
      const success = await store.RemoveAddress()
      if (success) {
        isConfirmDeleteOpen.value = false
        addressToDelete.value = null
        store.GetAddress()
      }
    }
  }

  const isDarkMode = ref(false)
  onMounted(async () => {
    console.log('Fetching Address...')
    await store.GetAddress()
    console.log('Address after Fetch:', store.fieldValues)
  })
</script>
<style>
  .dark {
    @apply bg-gray-900 text-white;
  }
</style>
