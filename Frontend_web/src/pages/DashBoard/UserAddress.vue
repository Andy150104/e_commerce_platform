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
          <div v-for="(address, index) in store.uUDSSelectUserAddressEntity" :key="index">
            <div class="flex flex-col sm:flex-row justify-between items-center pb-4">
              <div class="text-left">
                <div class="text-gray-700 dark:text-gray-300 font-bold flex items-center text-xl gap-2">
                  {{ address.firstName +' '+ address.lastName}}
                </div>
                <p class="text-gray-700 dark:text-gray-300">{{ address.ward }}</p>
                <p class="text-gray-700 dark:text-gray-300">{{ `${address.city}, ${address.province} ${address.district}` }}</p>
              </div>
              <div class="mt-4 flex flex-col items-end space-y-2">
                <h2
                  @click="updateAddress(address.addressId)"
                  class="cursor-pointer text-blue-500 hover:text-blue-600 font-semibold dark:text-blue-400 dark:hover:text-blue-300"
                >
                  Update Address
                </h2>
                <button @click="removeAddress(address.addressId)" class="bg-red-500 hover:bg-red-600 text-white font-semibold py-2 px-4 rounded">
                  Remove
                </button>
              </div>
            </div>
            <!-- Separator Line -->
            <hr v-if="index !== addresses.length - 1" class="border-gray-300 dark:border-gray-600 my-4" />
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>

<script lang="ts" setup>
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
import { useDashAddressStore } from '@PKG_SRC/stores/Modules/DashBoard/dashboardAddressStore'

import { useFieldArray, useForm } from 'vee-validate'
  import { ref } from 'vue'
  import { useRouter } from 'vue-router'


  const store = useDashAddressStore()
  interface Address {
    id: number
    name: string
    phoneNumber: string
    street: string
    city: string
    state: string
    zip: string
  }

  const router = useRouter()

  const addresses = ref<Address[]>([
    { id: 1, name: 'John Doe', phoneNumber: '(123) 456-7890', street: '123 Main St', city: 'New York', state: 'NY', zip: '10001' },
    { id: 2, name: 'Jane Smith', phoneNumber: '(987) 654-3210', street: '456 Elm St', city: 'Los Angeles', state: 'CA', zip: '90001' },
    { id: 3, name: 'Bob Johnson', phoneNumber: '(555) 123-4567', street: '789 Oak St', city: 'Chicago', state: 'IL', zip: '60601' },
  ])

  const addNewAddress = () => {
    const newAddress: Address = {
      id: addresses.value.length + 1,
      name: '',
      phoneNumber: '',
      street: '',
      city: '',
      state: '',
      zip: '',
    }
    addresses.value.push(newAddress)
  }

  const updateAddress = (id: string) => {
    // router.push({ name: 'UpdateAddress', params: { id: id.toString() } })
  }

  const removeAddress = (id: string) => {
    // addresses.value = addresses.value.filter((address) => address.id !== id)
  }

  const isDarkMode = ref(false)
  onMounted(() => {
    store.GetAddress()
  })
</script>

<style>
  .dark {
    @apply bg-gray-900 text-white;
  }
</style>
