/* eslint-disable */
export type District = {
  name: string
  code: number
  division_type: VietNamDivisionType
  codename: string
  province_code: number
  wards?: Ward[] | undefined
}

export type HTTPValidationError = {
  detail?: ValidationError[] | undefined
}

export type ProvinceResponse = {
  name: string
  code: number
  division_type: VietNamDivisionType
  codename: string
  phone_code: number
  districts?: District[] | undefined
}

export type SearchResult = {
  name: string
  code: number

  /** This info can help client side highlight the result in display. */
  matches?: {} | undefined
}

export type ValidationError = {
  loc: (string | number)[]
  msg: string
  type: string
}

export type VersionResponse = {
  data_version: string
}

export type VietNamDivisionType = 'tỉnh' | 'thành phố trung ương' | 'huyện' | 'quận' | 'thành phố' | 'thị xã' | 'xã' | 'thị trấn' | 'phường'

export type Ward = {
  name: string
  code: number
  division_type: VietNamDivisionType
  codename: string
  district_code: number
}
