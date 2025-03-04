import type { AspidaClient, BasicHeaders } from 'aspida'
import { dataToURLString } from 'aspida'
import type { Methods as Methods_1tc3et0 } from './api/v1/AEPSAddExchangeAccessory'
import type { Methods as Methods_17xh1oe } from './api/v1/DPSDeleteCartItem'
import type { Methods as Methods_aj9h04 } from './api/v1/DPSDeleteWishList'
import type { Methods as Methods_1kc1rtb } from './api/v1/DPSInsertCart'
import type { Methods as Methods_1hdxnaa } from './api/v1/DPSInsertWishList'
import type { Methods as Methods_ukb1ei } from './api/v1/DPSSelectAccessory'
import type { Methods as Methods_1pc2ej9 } from './api/v1/DPSSelectCartItem'
import type { Methods as Methods_1vgnjbr } from './api/v1/DPSSelectItem'
import type { Methods as Methods_t1ebqf } from './api/v1/DPSSelectWishList'
import type { Methods as Methods_1fhsfx0 } from './api/v1/DPSUpdateCartItem'
import type { Methods as Methods_1ixifv4 } from './api/v1/HSShowPlan'
import type { Methods as Methods_x7pqea } from './api/v1/MPSDeleteAccessory'
import type { Methods as Methods_bguy4h } from './api/v1/MPSDeleteImageAccessory'
import type { Methods as Methods_1jdop5c } from './api/v1/MPSInsertAccessory'
import type { Methods as Methods_12w961n } from './api/v1/MPSSelectAccessories'
import type { Methods as Methods_1sd1oup } from './api/v1/MSPInsertImageAccessory'
import type { Methods as Methods_1k8tpuv } from './api/v1/Momo'
import type { Methods as Methods_f44lv5 } from './api/v1/OPSAddPlanRefund'
import type { Methods as Methods_bvea9y } from './api/v1/OPSBuyingPlan'
import type { Methods as Methods_17gshg4 } from './api/v1/OPSRefundPlan'
import type { Methods as Methods_1othzpb } from './api/v1/UDSInsertUserAddress'
import type { Methods as Methods_1i0gbdw } from './api/v1/UDSSelectUserAddress'
import type { Methods as Methods_1smrvw1 } from './api/v1/UDSSelectUserProfile'
import type { Methods as Methods_pfkw7b } from './api/v1/UDSUpdateUserAddress'
import type { Methods as Methods_o6khhm } from './api/v1/UDSUpdateUserProfile'
import type { Methods as Methods_1bn80az } from './api/v1/URSUserRegister'
import type { Methods as Methods_7pt71i } from './api/v1/VEXSAddToQueue'
import type { Methods as Methods_10gaz9b } from './api/v1/mps/MPSUpdateAccessory'

const api = <T>({ baseURL, fetch }: AspidaClient<T>) => {
  const prefix = (baseURL === undefined ? 'https://localhost:5092' : baseURL).replace(/\/$/, '')
  const PATH0 = '/api/v1/AEPSAddExchangeAccessory'
  const PATH1 = '/api/v1/DPSDeleteCartItem'
  const PATH2 = '/api/v1/DPSDeleteWishList'
  const PATH3 = '/api/v1/DPSInsertCart'
  const PATH4 = '/api/v1/DPSInsertWishList'
  const PATH5 = '/api/v1/DPSSelectAccessory'
  const PATH6 = '/api/v1/DPSSelectCartItem'
  const PATH7 = '/api/v1/DPSSelectItem'
  const PATH8 = '/api/v1/DPSSelectWishList'
  const PATH9 = '/api/v1/DPSUpdateCartItem'
  const PATH10 = '/api/v1/HSShowPlan'
  const PATH11 = '/api/v1/MPSDeleteAccessory'
  const PATH12 = '/api/v1/MPSDeleteImageAccessory'
  const PATH13 = '/api/v1/MPSInsertAccessory'
  const PATH14 = '/api/v1/MPSSelectAccessories'
  const PATH15 = '/api/v1/MSPInsertImageAccessory'
  const PATH16 = '/api/v1/Momo'
  const PATH17 = '/api/v1/OPSAddPlanRefund'
  const PATH18 = '/api/v1/OPSBuyingPlan'
  const PATH19 = '/api/v1/OPSRefundPlan'
  const PATH20 = '/api/v1/UDSInsertUserAddress'
  const PATH21 = '/api/v1/UDSSelectUserAddress'
  const PATH22 = '/api/v1/UDSSelectUserProfile'
  const PATH23 = '/api/v1/UDSUpdateUserAddress'
  const PATH24 = '/api/v1/UDSUpdateUserProfile'
  const PATH25 = '/api/v1/URSUserRegister'
  const PATH26 = '/api/v1/VEXSAddToQueue'
  const PATH27 = '/api/v1/mps/MPSUpdateAccessory'
  const GET = 'GET'
  const POST = 'POST'

  return {
    api: {
      v1: {
        AEPSAddExchangeAccessory: {
          post: (option: { body: Methods_1tc3et0['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1tc3et0['post']['resBody'], BasicHeaders, Methods_1tc3et0['post']['status']>(prefix, PATH0, POST, option).json(),
          $post: (option: { body: Methods_1tc3et0['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1tc3et0['post']['resBody'], BasicHeaders, Methods_1tc3et0['post']['status']>(prefix, PATH0, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH0}`,
        },
        DPSDeleteCartItem: {
          post: (option: { body: Methods_17xh1oe['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_17xh1oe['post']['resBody'], BasicHeaders, Methods_17xh1oe['post']['status']>(prefix, PATH1, POST, option).json(),
          $post: (option: { body: Methods_17xh1oe['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_17xh1oe['post']['resBody'], BasicHeaders, Methods_17xh1oe['post']['status']>(prefix, PATH1, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH1}`,
        },
        DPSDeleteWishList: {
          post: (option: { body: Methods_aj9h04['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_aj9h04['post']['resBody'], BasicHeaders, Methods_aj9h04['post']['status']>(prefix, PATH2, POST, option).json(),
          $post: (option: { body: Methods_aj9h04['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_aj9h04['post']['resBody'], BasicHeaders, Methods_aj9h04['post']['status']>(prefix, PATH2, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH2}`,
        },
        DPSInsertCart: {
          post: (option: { body: Methods_1kc1rtb['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1kc1rtb['post']['resBody'], BasicHeaders, Methods_1kc1rtb['post']['status']>(prefix, PATH3, POST, option).json(),
          $post: (option: { body: Methods_1kc1rtb['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1kc1rtb['post']['resBody'], BasicHeaders, Methods_1kc1rtb['post']['status']>(prefix, PATH3, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH3}`,
        },
        DPSInsertWishList: {
          post: (option: { body: Methods_1hdxnaa['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1hdxnaa['post']['resBody'], BasicHeaders, Methods_1hdxnaa['post']['status']>(prefix, PATH4, POST, option).json(),
          $post: (option: { body: Methods_1hdxnaa['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1hdxnaa['post']['resBody'], BasicHeaders, Methods_1hdxnaa['post']['status']>(prefix, PATH4, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH4}`,
        },
        DPSSelectAccessory: {
          post: (option: { body: Methods_ukb1ei['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_ukb1ei['post']['resBody'], BasicHeaders, Methods_ukb1ei['post']['status']>(prefix, PATH5, POST, option).json(),
          $post: (option: { body: Methods_ukb1ei['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_ukb1ei['post']['resBody'], BasicHeaders, Methods_ukb1ei['post']['status']>(prefix, PATH5, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH5}`,
        },
        DPSSelectCartItem: {
          post: (option: { body: Methods_1pc2ej9['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1pc2ej9['post']['resBody'], BasicHeaders, Methods_1pc2ej9['post']['status']>(prefix, PATH6, POST, option).json(),
          $post: (option: { body: Methods_1pc2ej9['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1pc2ej9['post']['resBody'], BasicHeaders, Methods_1pc2ej9['post']['status']>(prefix, PATH6, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH6}`,
        },
        DPSSelectItem: {
          post: (option: { body: Methods_1vgnjbr['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1vgnjbr['post']['resBody'], BasicHeaders, Methods_1vgnjbr['post']['status']>(prefix, PATH7, POST, option).json(),
          $post: (option: { body: Methods_1vgnjbr['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1vgnjbr['post']['resBody'], BasicHeaders, Methods_1vgnjbr['post']['status']>(prefix, PATH7, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH7}`,
        },
        DPSSelectWishList: {
          post: (option: { body: Methods_t1ebqf['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_t1ebqf['post']['resBody'], BasicHeaders, Methods_t1ebqf['post']['status']>(prefix, PATH8, POST, option).json(),
          $post: (option: { body: Methods_t1ebqf['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_t1ebqf['post']['resBody'], BasicHeaders, Methods_t1ebqf['post']['status']>(prefix, PATH8, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH8}`,
        },
        DPSUpdateCartItem: {
          post: (option: { body: Methods_1fhsfx0['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1fhsfx0['post']['resBody'], BasicHeaders, Methods_1fhsfx0['post']['status']>(prefix, PATH9, POST, option).json(),
          $post: (option: { body: Methods_1fhsfx0['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1fhsfx0['post']['resBody'], BasicHeaders, Methods_1fhsfx0['post']['status']>(prefix, PATH9, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH9}`,
        },
        HSShowPlan: {
          post: (option: { body: Methods_1ixifv4['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1ixifv4['post']['resBody'], BasicHeaders, Methods_1ixifv4['post']['status']>(prefix, PATH10, POST, option).json(),
          $post: (option: { body: Methods_1ixifv4['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1ixifv4['post']['resBody'], BasicHeaders, Methods_1ixifv4['post']['status']>(prefix, PATH10, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH10}`,
        },
        MPSDeleteAccessory: {
          post: (option: { body: Methods_x7pqea['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_x7pqea['post']['resBody'], BasicHeaders, Methods_x7pqea['post']['status']>(prefix, PATH11, POST, option).json(),
          $post: (option: { body: Methods_x7pqea['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_x7pqea['post']['resBody'], BasicHeaders, Methods_x7pqea['post']['status']>(prefix, PATH11, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH11}`,
        },
        MPSDeleteImageAccessory: {
          post: (option: { body: Methods_bguy4h['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_bguy4h['post']['resBody'], BasicHeaders, Methods_bguy4h['post']['status']>(prefix, PATH12, POST, option).json(),
          $post: (option: { body: Methods_bguy4h['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_bguy4h['post']['resBody'], BasicHeaders, Methods_bguy4h['post']['status']>(prefix, PATH12, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH12}`,
        },
        MPSInsertAccessory: {
          post: (option: {
            body: Methods_1jdop5c['post']['reqBody']
            query?: Methods_1jdop5c['post']['query'] | undefined
            config?: T | undefined
          }) =>
            fetch<Methods_1jdop5c['post']['resBody'], BasicHeaders, Methods_1jdop5c['post']['status']>(
              prefix,
              PATH13,
              POST,
              option,
              'FormData'
            ).json(),
          $post: (option: {
            body: Methods_1jdop5c['post']['reqBody']
            query?: Methods_1jdop5c['post']['query'] | undefined
            config?: T | undefined
          }) =>
            fetch<Methods_1jdop5c['post']['resBody'], BasicHeaders, Methods_1jdop5c['post']['status']>(prefix, PATH13, POST, option, 'FormData')
              .json()
              .then((r) => r.body),
          $path: (option?: { method: 'post'; query: Methods_1jdop5c['post']['query'] } | undefined) =>
            `${prefix}${PATH13}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        MPSSelectAccessories: {
          post: (option: { body: Methods_12w961n['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_12w961n['post']['resBody'], BasicHeaders, Methods_12w961n['post']['status']>(prefix, PATH14, POST, option).json(),
          $post: (option: { body: Methods_12w961n['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_12w961n['post']['resBody'], BasicHeaders, Methods_12w961n['post']['status']>(prefix, PATH14, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH14}`,
        },
        MSPInsertImageAccessory: {
          post: (option: {
            body: Methods_1sd1oup['post']['reqBody']
            query?: Methods_1sd1oup['post']['query'] | undefined
            config?: T | undefined
          }) =>
            fetch<Methods_1sd1oup['post']['resBody'], BasicHeaders, Methods_1sd1oup['post']['status']>(
              prefix,
              PATH15,
              POST,
              option,
              'FormData'
            ).json(),
          $post: (option: {
            body: Methods_1sd1oup['post']['reqBody']
            query?: Methods_1sd1oup['post']['query'] | undefined
            config?: T | undefined
          }) =>
            fetch<Methods_1sd1oup['post']['resBody'], BasicHeaders, Methods_1sd1oup['post']['status']>(prefix, PATH15, POST, option, 'FormData')
              .json()
              .then((r) => r.body),
          $path: (option?: { method: 'post'; query: Methods_1sd1oup['post']['query'] } | undefined) =>
            `${prefix}${PATH15}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        Momo: {
          get: (option?: { config?: T | undefined } | undefined) =>
            fetch<Methods_1k8tpuv['get']['resBody'], BasicHeaders, Methods_1k8tpuv['get']['status']>(prefix, PATH16, GET, option).blob(),
          $get: (option?: { config?: T | undefined } | undefined) =>
            fetch<Methods_1k8tpuv['get']['resBody'], BasicHeaders, Methods_1k8tpuv['get']['status']>(prefix, PATH16, GET, option)
              .blob()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH16}`,
        },
        OPSAddPlanRefund: {
          post: (option: { body: Methods_f44lv5['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_f44lv5['post']['resBody'], BasicHeaders, Methods_f44lv5['post']['status']>(prefix, PATH17, POST, option).json(),
          $post: (option: { body: Methods_f44lv5['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_f44lv5['post']['resBody'], BasicHeaders, Methods_f44lv5['post']['status']>(prefix, PATH17, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH17}`,
        },
        OPSBuyingPlan: {
          post: (option: { body: Methods_bvea9y['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_bvea9y['post']['resBody'], BasicHeaders, Methods_bvea9y['post']['status']>(prefix, PATH18, POST, option).json(),
          $post: (option: { body: Methods_bvea9y['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_bvea9y['post']['resBody'], BasicHeaders, Methods_bvea9y['post']['status']>(prefix, PATH18, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH18}`,
        },
        OPSRefundPlan: {
          post: (option: { body: Methods_17gshg4['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_17gshg4['post']['resBody'], BasicHeaders, Methods_17gshg4['post']['status']>(prefix, PATH19, POST, option).json(),
          $post: (option: { body: Methods_17gshg4['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_17gshg4['post']['resBody'], BasicHeaders, Methods_17gshg4['post']['status']>(prefix, PATH19, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH19}`,
        },
        UDSInsertUserAddress: {
          post: (option: { body: Methods_1othzpb['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1othzpb['post']['resBody'], BasicHeaders, Methods_1othzpb['post']['status']>(prefix, PATH20, POST, option).json(),
          $post: (option: { body: Methods_1othzpb['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1othzpb['post']['resBody'], BasicHeaders, Methods_1othzpb['post']['status']>(prefix, PATH20, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH20}`,
        },
        UDSSelectUserAddress: {
          post: (option: { body: Methods_1i0gbdw['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1i0gbdw['post']['resBody'], BasicHeaders, Methods_1i0gbdw['post']['status']>(prefix, PATH21, POST, option).json(),
          $post: (option: { body: Methods_1i0gbdw['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1i0gbdw['post']['resBody'], BasicHeaders, Methods_1i0gbdw['post']['status']>(prefix, PATH21, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH21}`,
        },
        UDSSelectUserProfile: {
          post: (option: { body: Methods_1smrvw1['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1smrvw1['post']['resBody'], BasicHeaders, Methods_1smrvw1['post']['status']>(prefix, PATH22, POST, option).json(),
          $post: (option: { body: Methods_1smrvw1['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1smrvw1['post']['resBody'], BasicHeaders, Methods_1smrvw1['post']['status']>(prefix, PATH22, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH22}`,
        },
        UDSUpdateUserAddress: {
          post: (option: { body: Methods_pfkw7b['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_pfkw7b['post']['resBody'], BasicHeaders, Methods_pfkw7b['post']['status']>(prefix, PATH23, POST, option).json(),
          $post: (option: { body: Methods_pfkw7b['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_pfkw7b['post']['resBody'], BasicHeaders, Methods_pfkw7b['post']['status']>(prefix, PATH23, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH23}`,
        },
        UDSUpdateUserProfile: {
          post: (option: { body: Methods_o6khhm['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_o6khhm['post']['resBody'], BasicHeaders, Methods_o6khhm['post']['status']>(prefix, PATH24, POST, option).json(),
          $post: (option: { body: Methods_o6khhm['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_o6khhm['post']['resBody'], BasicHeaders, Methods_o6khhm['post']['status']>(prefix, PATH24, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH24}`,
        },
        URSUserRegister: {
          post: (option: { body: Methods_1bn80az['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1bn80az['post']['resBody'], BasicHeaders, Methods_1bn80az['post']['status']>(prefix, PATH25, POST, option).json(),
          $post: (option: { body: Methods_1bn80az['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_1bn80az['post']['resBody'], BasicHeaders, Methods_1bn80az['post']['status']>(prefix, PATH25, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH25}`,
        },
        VEXSAddToQueue: {
          post: (option: { body: Methods_7pt71i['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_7pt71i['post']['resBody'], BasicHeaders, Methods_7pt71i['post']['status']>(prefix, PATH26, POST, option).json(),
          $post: (option: { body: Methods_7pt71i['post']['reqBody']; config?: T | undefined }) =>
            fetch<Methods_7pt71i['post']['resBody'], BasicHeaders, Methods_7pt71i['post']['status']>(prefix, PATH26, POST, option)
              .json()
              .then((r) => r.body),
          $path: () => `${prefix}${PATH26}`,
        },
        mps: {
          MPSUpdateAccessory: {
            post: (option: {
              body: Methods_10gaz9b['post']['reqBody']
              query?: Methods_10gaz9b['post']['query'] | undefined
              config?: T | undefined
            }) =>
              fetch<Methods_10gaz9b['post']['resBody'], BasicHeaders, Methods_10gaz9b['post']['status']>(
                prefix,
                PATH27,
                POST,
                option,
                'FormData'
              ).json(),
            $post: (option: {
              body: Methods_10gaz9b['post']['reqBody']
              query?: Methods_10gaz9b['post']['query'] | undefined
              config?: T | undefined
            }) =>
              fetch<Methods_10gaz9b['post']['resBody'], BasicHeaders, Methods_10gaz9b['post']['status']>(prefix, PATH27, POST, option, 'FormData')
                .json()
                .then((r) => r.body),
            $path: (option?: { method: 'post'; query: Methods_10gaz9b['post']['query'] } | undefined) =>
              `${prefix}${PATH27}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
          },
        },
      },
    },
  }
}

export type ApiInstance = ReturnType<typeof api>
export default api
