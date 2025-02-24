<template>
  <table id="sorting-table" class="w-full text-sm text-left text-gray-500">
    <thead class="text-xs text-gray-700 uppercase bg-gray-50">
  <tr>
    <th class="px-6 py-3 cursor-pointer" @click="sortBy('country')">
      <span class="flex items-center">
        Country
        <svg
          class="w-4 h-4 ms-1 transform transition-transform duration-200"
          :class="{'rotate-180': sortKey === 'country' && sortOrder === 'desc'}"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M5 15l7-7 7 7"/>
        </svg>
      </span>
    </th>
    <th class="px-6 py-3 cursor-pointer" @click="sortBy('gdp')">
      <span class="flex items-center">
        GDP
        <svg
          class="w-4 h-4 ms-1 transform transition-transform duration-200"
          :class="{'rotate-180': sortKey === 'gdp' && sortOrder === 'desc'}"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M5 15l7-7 7 7"/>
        </svg>
      </span>
    </th>
    <th class="px-6 py-3 cursor-pointer" @click="sortBy('population')">
      <span class="flex items-center">
        Population
        <svg
          class="w-4 h-4 ms-1 transform transition-transform duration-200"
          :class="{'rotate-180': sortKey === 'population' && sortOrder === 'desc'}"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M5 15l7-7 7 7"/>
        </svg>
      </span>
    </th>
    <th class="px-6 py-3 cursor-pointer" @click="sortBy('gdpPerCapita')">
      <span class="flex items-center">
        GDP per Capita
        <svg
          class="w-4 h-4 ms-1 transform transition-transform duration-200"
          :class="{'rotate-180': sortKey === 'gdpPerCapita' && sortOrder === 'desc'}"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
          stroke-width="2">
          <path stroke-linecap="round" stroke-linejoin="round" d="M5 15l7-7 7 7"/>
        </svg>
      </span>
    </th>
    <th class="px-6 py-3"></th>
  </tr>
</thead>

    <tbody>
      <!-- Duyệt qua các hàng đã được sắp xếp -->
      <template v-for="row in sortedRows" :key="row.id">
        <tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
          <td class="px-6 py-4 font-medium text-gray-900 dark:text-white">{{ row.country }}</td>
          <td class="px-6 py-4">{{ row.gdp }}</td>
          <td class="px-6 py-4">{{ row.population }}</td>
          <td class="px-6 py-4">{{ row.gdpPerCapita }}</td>
          <td class="px-6 py-4">
            <button @click="toggleRow(row.id)" class="focus:outline-none">
              <svg
                class="w-4 h-4 transform transition-transform duration-200"
                :class="{ 'rotate-180': expandedRows.includes(row.id) }"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
                stroke-width="2"
              >
                <path stroke-linecap="round" stroke-linejoin="round" d="M19 9l-7 7-7-7" />
              </svg>
            </button>
          </td>
        </tr>
        <!-- Hàng mở rộng -->
        <tr v-if="expandedRows.includes(row.id)" class="bg-gray-50 dark:bg-gray-700">
          <td colspan="5" class="px-6 py-4">
            <div class="p-4 bg-gray-100 border border-gray-300 rounded-md">
              <strong>Details for {{ row.country }}:</strong>
              <p>Đây là nội dung chi tiết của {{ row.country }}. Bạn có thể hiển thị thêm thông tin tại đây.</p>
            </div>
          </td>
        </tr>
      </template>
    </tbody>
  </table>
</template>

<script setup lang="ts">
  import { ref, computed } from 'vue'

  interface RowData {
    id: number
    country: string
    gdp: string
    population: string
    gdpPerCapita: string
  }

  const rows = ref<RowData[]>([
    { id: 1, country: 'United States', gdp: '$21433 billion', population: '331 million', gdpPerCapita: '$64763' },
    { id: 2, country: 'China', gdp: '$14342 billion', population: '1441 million', gdpPerCapita: '$9957' },
    { id: 3, country: 'Japan', gdp: '$5082 billion', population: '126 million', gdpPerCapita: '$40335' },
    { id: 4, country: 'Germany', gdp: '$3846 billion', population: '83 million', gdpPerCapita: '$46386' },
    { id: 5, country: 'India', gdp: '$2875 billion', population: '1380 million', gdpPerCapita: '$2083' },
    { id: 6, country: 'United Kingdom', gdp: '$2829 billion', population: '67 million', gdpPerCapita: '$42224' },
    // Thêm các dữ liệu khác nếu cần
  ])

  // Quản lý trạng thái mở rộng của các hàng
  const expandedRows = ref<number[]>([])

  // Biến lưu trữ key cột đang sắp xếp và thứ tự sắp xếp (asc/desc)
  const sortKey = ref<string>('') // Mặc định không sắp xếp
  const sortOrder = ref<'asc' | 'desc'>('asc')

  function toggleRow(id: number) {
    if (expandedRows.value.includes(id)) {
      expandedRows.value = expandedRows.value.filter((rowId) => rowId !== id)
    } else {
      expandedRows.value.push(id)
    }
  }

  function sortBy(key: string) {
    if (sortKey.value === key) {
      // Nếu nhấn vào cùng cột, đổi hướng sắp xếp
      sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc'
    } else {
      sortKey.value = key
      sortOrder.value = 'asc'
    }
  }

  const sortedRows = computed(() => {
    if (!sortKey.value) {
      return rows.value
    }
    // Tạo bản sao của rows để không thay đổi dữ liệu gốc
    const sorted = [...rows.value]
    sorted.sort((a, b) => {
      const aValue = a[sortKey.value as keyof RowData]
      const bValue = b[sortKey.value as keyof RowData]
      if (aValue < bValue) {
        return sortOrder.value === 'asc' ? -1 : 1
      }
      if (aValue > bValue) {
        return sortOrder.value === 'asc' ? 1 : -1
      }
      return 0
    })
    return sorted
  })
  
</script>
