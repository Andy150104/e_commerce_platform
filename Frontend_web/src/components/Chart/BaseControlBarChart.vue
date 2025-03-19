<template>
      <Chart type="bar" :data="computedChartData" :options="computedChartOptions" class="w-full h-[30rem]" />
  </template>
  
  <script lang="ts" setup>
  import { computed } from 'vue'
  
  interface ChartDataType {
    labels: string[]
    datasets: any[]
  }
  
  interface ChartOptionsType {
    maintainAspectRatio: boolean
    aspectRatio: number
    plugins: Record<string, any>
    scales: Record<string, any>
  }
  
  // Định nghĩa props để có thể truyền data từ ngoài vào
  const props = defineProps<{
    chartData?: ChartDataType
    chartOptions?: ChartOptionsType
  }>()
  
  // Lấy style từ document để dùng cho default data/options
  const documentStyle = getComputedStyle(document.documentElement)
  
  // Hàm tạo dữ liệu mặc định cho Chart
  const defaultChartData = (): ChartDataType => ({
    labels: [
      'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 
      'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
    ],
    datasets: [
      {
        type: 'line',
        label: 'Doanh thu (triệu đồng)',
        borderColor: documentStyle.getPropertyValue('--p-orange-500'),
        borderWidth: 2,
        fill: false,
        tension: 0.4,
        data: [30, 45, 40, 60, 55, 65, 70]
      },
      {
        type: 'bar',
        label: 'Chi phí (triệu đồng)',
        backgroundColor: documentStyle.getPropertyValue('--p-gray-500'),
        data: [15, 20, 25, 30, 28, 35, 32],
        borderColor: 'white',
        borderWidth: 2
      },
      {
        type: 'bar',
        label: 'Lợi nhuận (triệu đồng)',
        backgroundColor: documentStyle.getPropertyValue('--p-cyan-500'),
        data: [15, 25, 15, 30, 27, 30, 38]
      }
    ]
  })
  
  // Hàm tạo options mặc định cho Chart
  const defaultChartOptions = (): ChartOptionsType => {
    const textColor = documentStyle.getPropertyValue('--p-text-color')
    const textColorSecondary = documentStyle.getPropertyValue('--p-text-muted-color')
    const surfaceBorder = documentStyle.getPropertyValue('--p-content-border-color')
  
    return {
      maintainAspectRatio: false,
      aspectRatio: 0.6,
      plugins: {
        legend: {
          labels: {
            color: textColor
          }
        }
      },
      scales: {
        x: {
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder
          }
        },
        y: {
          ticks: {
            color: textColorSecondary
          },
          grid: {
            color: surfaceBorder
          }
        }
      }
    }
  }
  
  // Sử dụng computed để ưu tiên sử dụng props nếu được truyền, ngược lại dùng default
  const computedChartData = computed(() => props.chartData || defaultChartData())
  const computedChartOptions = computed(() => props.chartOptions || defaultChartOptions())
  </script>
  