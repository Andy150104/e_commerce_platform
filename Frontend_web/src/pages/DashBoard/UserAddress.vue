<template>
  <BaseScreenDashBoard>
    <template #body>
      <div class="container mx-auto px-4 py-8">
        <div class="flex justify-between items-center mr-5 ml-6 mb-6">
          <h1 class="text-2xl font-bold dark:text-white">My Addresses</h1>
          <ModalInputForm
            :header="'Add New Address'"
            :button-icon="'pi pi-plus'"
            :button-label="'Add New Address'"
            :button-severity="'contrast'"
            :cancel-label="'Cancel'"
            :confirm-label="'Add'"
            :width="'60rem'"
            @on-confirm="addAddress"
          >
            <template #body>
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
                  <LocationPicker
                    :xml-column-province="xmlColumns.province"
                    :maxlength-province="50"
                    :disabled-province="false"
                    :err-msg-province="fieldErrors.province"
                    :placeholder-province="'Province'"
                    :xml-column-district="xmlColumns.district"
                    :maxlength-district="50"
                    :disabled-district="false"
                    :err-msg-district="fieldErrors.district"
                    :placeholder-district="'District'"
                    :xml-column-ward="xmlColumns.ward"
                    :maxlength-ward="50"
                    :disabled-ward="false"
                    :err-msg-ward="fieldErrors.ward"
                    :placeholder-ward="'Ward'"
                  />
                </div>
              </div>
            </template>
          </ModalInputForm>
        </div>
        <div class="bg-white dark:bg-gray-800 shadow-md rounded-lg p-6">
          <div v-for="(address, index) in store.uUDSSelectUserAddressEntity ?? []" :key="index">
            <div class="flex flex-col sm:flex-row justify-between items-center pb-4">
              <div class="text-left">
                <div class="text-gray-700 dark:text-gray-300 font-bold flex items-center text-xl gap-2">
                  {{ (address?.firstName ?? '') + ' ' + (address?.lastName ?? '') }}
                </div>
                <p class="text-gray-700 dark:text-gray-300">{{ address.addressLine }}</p>
                <p class="text-gray-700 dark:text-gray-300">{{ address.ward }}</p>
                <p class="text-gray-700 dark:text-gray-300">
                  {{
                    `${address.city}, ${address.province}
                  ${address.district}`
                  }}
                </p>
              </div>
              <div class="mt-4 flex flex-col items-end space-y-2">
                <ModalInputForm
                  :header="'Update Address'"
                  :button-icon="'pi pi-pencil'"
                  :button-label="'Update address'"
                  :button-severity="'Secondary'"
                  :cancel-label="'Cancel'"
                  :confirm-label="'Update'"
                  :width="'60rem'"
                  @on-blinding-update="onBinding(address.addressId ?? '')"
                  @on-confirm="updateAddress"
                >
                  <template #body>
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
                        <LocationPicker
                          :xml-column-province="xmlColumns.province"
                          :maxlength-province="50"
                          :disabled-province="false"
                          :err-msg-province="fieldErrors.province"
                          :placeholder-province="'Province'"
                          :xml-column-district="xmlColumns.district"
                          :maxlength-district="50"
                          :disabled-district="false"
                          :err-msg-district="fieldErrors.district"
                          :placeholder-district="'District'"
                          :xml-column-ward="xmlColumns.ward"
                          :maxlength-ward="50"
                          :disabled-ward="false"
                          :err-msg-ward="fieldErrors.ward"
                          :placeholder-ward="'Ward'"
                        />
                      </div>
                    </div>
                  </template>
                </ModalInputForm>
                <ModalConfirm @on-confirm="removeAddress(address.addressId ?? '')" />
              </div>
            </div>
            <hr v-if="index < store.uUDSSelectUserAddressEntity.length - 1" class="border-gray-300 dark:border-gray-600 my-4" />
          </div>
          <div
            v-if="!store.uUDSSelectUserAddressEntity?.length"
            class="bg-white dark:bg-gray-800 shadow-md rounded-lg p-6 flex justify-center items-center h-32"
          >
            <p class="text-gray-700 dark:text-gray-300 text-lg font-semibold">Your Address Book is empty.</p>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>

<script lang="ts" setup>
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'
  import { storeToRefs } from 'pinia'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import { ref, onMounted } from 'vue'
  import 'primeicons/primeicons.css'
  import { useDashAddressStore } from '@PKG_SRC/stores/Modules/DashBoard/dashboardAddressStore'
  import ModalInputForm from '@PKG_SRC/components/Modal/ModalInputForm.vue'
  import LocationPicker from '@PKG_SRC/components/UserControl/LocationPicker.vue'
  import ModalConfirm from '@PKG_SRC/components/Modal/ModalConfirm.vue'
  const store = useDashAddressStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)

  const xmlColumns = {
    addressId: XmlLoadColumn({
      id: 'addressId',
      name: 'Address Id',
      rules: 'required',
      visible: true,
      option: '',
    }),
    addressLine: XmlLoadColumn({
      id: 'addressLine',
      name: 'Address Line',
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

  // Binding data to field
  const onBinding = async (addressId: string) => {
    const address = await store.uUDSSelectUserAddressEntity.find((a) => a.addressId === addressId)
    console.log('ADDRESS ID : ' + address?.addressId)
    store.fields.setFieldValue('addressId', address?.addressId)
    store.fields.setFieldValue('addressLine', address?.addressLine)
    store.fields.setFieldValue('ward', address?.ward)
    store.fields.setFieldValue('province', address?.province)
    store.fields.setFieldValue('district', address?.district)
  }

  const addAddress = async () => {
    await store.InsertAddress()
    await store.GetAddress()
  }

  const removeAddress = async (addressId: string) => {
    const success = await store.RemoveAddress(addressId)
    if (success) {
      store.GetAddress()
    }
  }
  const updateAddress = async () => {
    const success = await store.UpdateAddress()
    if (success) {
      store.GetAddress()
    }
  }

  onMounted(async () => {
    await store.GetAddress()
    store.fields.setFieldValue('addressId', '2')
  })
</script>
