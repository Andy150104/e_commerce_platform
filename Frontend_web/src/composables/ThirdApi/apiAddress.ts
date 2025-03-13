import axios from 'axios'
import aspida from '@aspida/axios'
import api from './define/$api'

export const useApiAddress = () => {
  const runtimeConfig = useRuntimeConfig()
  const baseURL = 'https://provinces.open-api.vn'
  const apiClientAxios = axios.create({
    baseURL,
  })
  const apiServer = api(aspida(apiClientAxios))

  return apiServer
}
