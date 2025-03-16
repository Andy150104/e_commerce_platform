import type { AspidaClient, BasicHeaders } from 'aspida';
import { dataToURLString } from 'aspida';
import type { Methods as Methods_1tc3et0 } from './api/v1/AEPSAddExchangeAccessory';
import type { Methods as Methods_8kfr6z } from './api/v1/CreateCategory';
import type { Methods as Methods_17xh1oe } from './api/v1/DPSDeleteCartItem';
import type { Methods as Methods_aj9h04 } from './api/v1/DPSDeleteWishList';
import type { Methods as Methods_1kc1rtb } from './api/v1/DPSInsertCart';
import type { Methods as Methods_1hdxnaa } from './api/v1/DPSInsertWishList';
import type { Methods as Methods_ukb1ei } from './api/v1/DPSSelectAccessory';
import type { Methods as Methods_1pc2ej9 } from './api/v1/DPSSelectCartItem';
import type { Methods as Methods_1vgnjbr } from './api/v1/DPSSelectItem';
import type { Methods as Methods_t1ebqf } from './api/v1/DPSSelectWishList';
import type { Methods as Methods_1fhsfx0 } from './api/v1/DPSUpdateCartItem';
import type { Methods as Methods_16nxlok } from './api/v1/DeleteCategory';
import type { Methods as Methods_1ixifv4 } from './api/v1/HSShowPlan';
import type { Methods as Methods_3vv64e } from './api/v1/InsertOrder';
import type { Methods as Methods_x7pqea } from './api/v1/MPSDeleteAccessory';
import type { Methods as Methods_bguy4h } from './api/v1/MPSDeleteImageAccessory';
import type { Methods as Methods_1jdop5c } from './api/v1/MPSInsertAccessory';
import type { Methods as Methods_12w961n } from './api/v1/MPSSelectAccessories';
import type { Methods as Methods_13dmkbk } from './api/v1/MPSUpdateAccessory';
import type { Methods as Methods_1sd1oup } from './api/v1/MSPInsertImageAccessory';
import type { Methods as Methods_1k8tpuv } from './api/v1/Momo';
import type { Methods as Methods_vss6uo } from './api/v1/Momo/Order';
import type { Methods as Methods_f44lv5 } from './api/v1/OPSAddPlanRefund';
import type { Methods as Methods_bvea9y } from './api/v1/OPSBuyingPlan';
import type { Methods as Methods_9j6uh0 } from './api/v1/RROApproveRefundRequestOrder';
import type { Methods as Methods_vucz9i } from './api/v1/RRODeleteRefundRequestOrder';
import type { Methods as Methods_1lte7bs } from './api/v1/RROInsertRefundRequestOrder';
import type { Methods as Methods_uzk94p } from './api/v1/RROSelectRefundRequestOrder';
import type { Methods as Methods_772ni6 } from './api/v1/RROSelectRefundRequestOrders';
import type { Methods as Methods_14wigjo } from './api/v1/RROUpdateRefundRequestOrder';
import type { Methods as Methods_1ywrqxf } from './api/v1/SelectCategories';
import type { Methods as Methods_wgnsfn } from './api/v1/SelectCategory';
import type { Methods as Methods_16r358v } from './api/v1/SelectSubCategories';
import type { Methods as Methods_ecpjzp } from './api/v1/UDSDeleteUserAddress';
import type { Methods as Methods_1othzpb } from './api/v1/UDSInsertUserAddress';
import type { Methods as Methods_1i0gbdw } from './api/v1/UDSSelectUserAddress';
import type { Methods as Methods_1smrvw1 } from './api/v1/UDSSelectUserProfile';
import type { Methods as Methods_pfkw7b } from './api/v1/UDSUpdateUserAddress';
import type { Methods as Methods_o6khhm } from './api/v1/UDSUpdateUserProfile';
import type { Methods as Methods_1bn80az } from './api/v1/URSUserRegister';
import type { Methods as Methods_t8kgc6 } from './api/v1/UpdateCategory';
import type { Methods as Methods_7pt71i } from './api/v1/VEXSAddToQueue';

const api = <T>({ baseURL, fetch }: AspidaClient<T>) => {
  const prefix = (baseURL === undefined ? 'https://localhost:5092' : baseURL).replace(/\/$/, '');
  const PATH0 = '/api/v1/AEPSAddExchangeAccessory';
  const PATH1 = '/api/v1/CreateCategory';
  const PATH2 = '/api/v1/DPSDeleteCartItem';
  const PATH3 = '/api/v1/DPSDeleteWishList';
  const PATH4 = '/api/v1/DPSInsertCart';
  const PATH5 = '/api/v1/DPSInsertWishList';
  const PATH6 = '/api/v1/DPSSelectAccessory';
  const PATH7 = '/api/v1/DPSSelectCartItem';
  const PATH8 = '/api/v1/DPSSelectItem';
  const PATH9 = '/api/v1/DPSSelectWishList';
  const PATH10 = '/api/v1/DPSUpdateCartItem';
  const PATH11 = '/api/v1/DeleteCategory';
  const PATH12 = '/api/v1/HSShowPlan';
  const PATH13 = '/api/v1/InsertOrder';
  const PATH14 = '/api/v1/MPSDeleteAccessory';
  const PATH15 = '/api/v1/MPSDeleteImageAccessory';
  const PATH16 = '/api/v1/MPSInsertAccessory';
  const PATH17 = '/api/v1/MPSSelectAccessories';
  const PATH18 = '/api/v1/MPSUpdateAccessory';
  const PATH19 = '/api/v1/MSPInsertImageAccessory';
  const PATH20 = '/api/v1/Momo';
  const PATH21 = '/api/v1/Momo/Order';
  const PATH22 = '/api/v1/OPSAddPlanRefund';
  const PATH23 = '/api/v1/OPSBuyingPlan';
  const PATH24 = '/api/v1/RROApproveRefundRequestOrder';
  const PATH25 = '/api/v1/RRODeleteRefundRequestOrder';
  const PATH26 = '/api/v1/RROInsertRefundRequestOrder';
  const PATH27 = '/api/v1/RROSelectRefundRequestOrder';
  const PATH28 = '/api/v1/RROSelectRefundRequestOrders';
  const PATH29 = '/api/v1/RROUpdateRefundRequestOrder';
  const PATH30 = '/api/v1/SelectCategories';
  const PATH31 = '/api/v1/SelectCategory';
  const PATH32 = '/api/v1/SelectSubCategories';
  const PATH33 = '/api/v1/UDSDeleteUserAddress';
  const PATH34 = '/api/v1/UDSInsertUserAddress';
  const PATH35 = '/api/v1/UDSSelectUserAddress';
  const PATH36 = '/api/v1/UDSSelectUserProfile';
  const PATH37 = '/api/v1/UDSUpdateUserAddress';
  const PATH38 = '/api/v1/UDSUpdateUserProfile';
  const PATH39 = '/api/v1/URSUserRegister';
  const PATH40 = '/api/v1/UpdateCategory';
  const PATH41 = '/api/v1/VEXSAddToQueue';
  const GET = 'GET';
  const POST = 'POST';
  const PUT = 'PUT';
  const PATCH = 'PATCH';

  return {
    api: {
      v1: {
        AEPSAddExchangeAccessory: {
          post: (option: { body: Methods_1tc3et0['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1tc3et0['post']['resBody'], BasicHeaders, Methods_1tc3et0['post']['status']>(prefix, PATH0, POST, option).json(),
          $post: (option: { body: Methods_1tc3et0['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1tc3et0['post']['resBody'], BasicHeaders, Methods_1tc3et0['post']['status']>(prefix, PATH0, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH0}`,
        },
        CreateCategory: {
          post: (option: { body: Methods_8kfr6z['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_8kfr6z['post']['resBody'], BasicHeaders, Methods_8kfr6z['post']['status']>(prefix, PATH1, POST, option).json(),
          $post: (option: { body: Methods_8kfr6z['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_8kfr6z['post']['resBody'], BasicHeaders, Methods_8kfr6z['post']['status']>(prefix, PATH1, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH1}`,
        },
        DPSDeleteCartItem: {
          patch: (option: { body: Methods_17xh1oe['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_17xh1oe['patch']['resBody'], BasicHeaders, Methods_17xh1oe['patch']['status']>(prefix, PATH2, PATCH, option).json(),
          $patch: (option: { body: Methods_17xh1oe['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_17xh1oe['patch']['resBody'], BasicHeaders, Methods_17xh1oe['patch']['status']>(prefix, PATH2, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH2}`,
        },
        DPSDeleteWishList: {
          patch: (option: { body: Methods_aj9h04['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_aj9h04['patch']['resBody'], BasicHeaders, Methods_aj9h04['patch']['status']>(prefix, PATH3, PATCH, option).json(),
          $patch: (option: { body: Methods_aj9h04['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_aj9h04['patch']['resBody'], BasicHeaders, Methods_aj9h04['patch']['status']>(prefix, PATH3, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH3}`,
        },
        DPSInsertCart: {
          post: (option: { body: Methods_1kc1rtb['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1kc1rtb['post']['resBody'], BasicHeaders, Methods_1kc1rtb['post']['status']>(prefix, PATH4, POST, option).json(),
          $post: (option: { body: Methods_1kc1rtb['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1kc1rtb['post']['resBody'], BasicHeaders, Methods_1kc1rtb['post']['status']>(prefix, PATH4, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH4}`,
        },
        DPSInsertWishList: {
          post: (option: { body: Methods_1hdxnaa['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1hdxnaa['post']['resBody'], BasicHeaders, Methods_1hdxnaa['post']['status']>(prefix, PATH5, POST, option).json(),
          $post: (option: { body: Methods_1hdxnaa['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1hdxnaa['post']['resBody'], BasicHeaders, Methods_1hdxnaa['post']['status']>(prefix, PATH5, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH5}`,
        },
        DPSSelectAccessory: {
          get: (option?: { query?: Methods_ukb1ei['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_ukb1ei['get']['resBody'], BasicHeaders, Methods_ukb1ei['get']['status']>(prefix, PATH6, GET, option).json(),
          $get: (option?: { query?: Methods_ukb1ei['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_ukb1ei['get']['resBody'], BasicHeaders, Methods_ukb1ei['get']['status']>(prefix, PATH6, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_ukb1ei['get']['query'] } | undefined) =>
            `${prefix}${PATH6}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        DPSSelectCartItem: {
          get: (option?: { query?: Methods_1pc2ej9['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1pc2ej9['get']['resBody'], BasicHeaders, Methods_1pc2ej9['get']['status']>(prefix, PATH7, GET, option).json(),
          $get: (option?: { query?: Methods_1pc2ej9['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1pc2ej9['get']['resBody'], BasicHeaders, Methods_1pc2ej9['get']['status']>(prefix, PATH7, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1pc2ej9['get']['query'] } | undefined) =>
            `${prefix}${PATH7}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        DPSSelectItem: {
          get: (option?: { query?: Methods_1vgnjbr['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1vgnjbr['get']['resBody'], BasicHeaders, Methods_1vgnjbr['get']['status']>(prefix, PATH8, GET, option).json(),
          $get: (option?: { query?: Methods_1vgnjbr['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1vgnjbr['get']['resBody'], BasicHeaders, Methods_1vgnjbr['get']['status']>(prefix, PATH8, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1vgnjbr['get']['query'] } | undefined) =>
            `${prefix}${PATH8}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        DPSSelectWishList: {
          get: (option?: { query?: Methods_t1ebqf['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_t1ebqf['get']['resBody'], BasicHeaders, Methods_t1ebqf['get']['status']>(prefix, PATH9, GET, option).json(),
          $get: (option?: { query?: Methods_t1ebqf['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_t1ebqf['get']['resBody'], BasicHeaders, Methods_t1ebqf['get']['status']>(prefix, PATH9, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_t1ebqf['get']['query'] } | undefined) =>
            `${prefix}${PATH9}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        DPSUpdateCartItem: {
          patch: (option: { body: Methods_1fhsfx0['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1fhsfx0['patch']['resBody'], BasicHeaders, Methods_1fhsfx0['patch']['status']>(prefix, PATH10, PATCH, option).json(),
          $patch: (option: { body: Methods_1fhsfx0['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1fhsfx0['patch']['resBody'], BasicHeaders, Methods_1fhsfx0['patch']['status']>(prefix, PATH10, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH10}`,
        },
        DeleteCategory: {
          patch: (option: { body: Methods_16nxlok['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_16nxlok['patch']['resBody'], BasicHeaders, Methods_16nxlok['patch']['status']>(prefix, PATH11, PATCH, option).json(),
          $patch: (option: { body: Methods_16nxlok['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_16nxlok['patch']['resBody'], BasicHeaders, Methods_16nxlok['patch']['status']>(prefix, PATH11, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH11}`,
        },
        HSShowPlan: {
          get: (option?: { query?: Methods_1ixifv4['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1ixifv4['get']['resBody'], BasicHeaders, Methods_1ixifv4['get']['status']>(prefix, PATH12, GET, option).json(),
          $get: (option?: { query?: Methods_1ixifv4['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1ixifv4['get']['resBody'], BasicHeaders, Methods_1ixifv4['get']['status']>(prefix, PATH12, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1ixifv4['get']['query'] } | undefined) =>
            `${prefix}${PATH12}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        InsertOrder: {
          post: (option: { body: Methods_3vv64e['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_3vv64e['post']['resBody'], BasicHeaders, Methods_3vv64e['post']['status']>(prefix, PATH13, POST, option).json(),
          $post: (option: { body: Methods_3vv64e['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_3vv64e['post']['resBody'], BasicHeaders, Methods_3vv64e['post']['status']>(prefix, PATH13, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH13}`,
        },
        MPSDeleteAccessory: {
          patch: (option: { body: Methods_x7pqea['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_x7pqea['patch']['resBody'], BasicHeaders, Methods_x7pqea['patch']['status']>(prefix, PATH14, PATCH, option).json(),
          $patch: (option: { body: Methods_x7pqea['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_x7pqea['patch']['resBody'], BasicHeaders, Methods_x7pqea['patch']['status']>(prefix, PATH14, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH14}`,
        },
        MPSDeleteImageAccessory: {
          patch: (option: { body: Methods_bguy4h['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_bguy4h['patch']['resBody'], BasicHeaders, Methods_bguy4h['patch']['status']>(prefix, PATH15, PATCH, option).json(),
          $patch: (option: { body: Methods_bguy4h['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_bguy4h['patch']['resBody'], BasicHeaders, Methods_bguy4h['patch']['status']>(prefix, PATH15, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH15}`,
        },
        MPSInsertAccessory: {
          post: (option: { body: Methods_1jdop5c['post']['reqBody'], query?: Methods_1jdop5c['post']['query'] | undefined, config?: T | undefined }) =>
            fetch<Methods_1jdop5c['post']['resBody'], BasicHeaders, Methods_1jdop5c['post']['status']>(prefix, PATH16, POST, option, 'FormData').json(),
          $post: (option: { body: Methods_1jdop5c['post']['reqBody'], query?: Methods_1jdop5c['post']['query'] | undefined, config?: T | undefined }) =>
            fetch<Methods_1jdop5c['post']['resBody'], BasicHeaders, Methods_1jdop5c['post']['status']>(prefix, PATH16, POST, option, 'FormData').json().then(r => r.body),
          $path: (option?: { method: 'post'; query: Methods_1jdop5c['post']['query'] } | undefined) =>
            `${prefix}${PATH16}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        MPSSelectAccessories: {
          get: (option?: { query?: Methods_12w961n['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_12w961n['get']['resBody'], BasicHeaders, Methods_12w961n['get']['status']>(prefix, PATH17, GET, option).json(),
          $get: (option?: { query?: Methods_12w961n['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_12w961n['get']['resBody'], BasicHeaders, Methods_12w961n['get']['status']>(prefix, PATH17, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_12w961n['get']['query'] } | undefined) =>
            `${prefix}${PATH17}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        MPSUpdateAccessory: {
          put: (option: { body: Methods_13dmkbk['put']['reqBody'], query?: Methods_13dmkbk['put']['query'] | undefined, config?: T | undefined }) =>
            fetch<Methods_13dmkbk['put']['resBody'], BasicHeaders, Methods_13dmkbk['put']['status']>(prefix, PATH18, PUT, option, 'FormData').json(),
          $put: (option: { body: Methods_13dmkbk['put']['reqBody'], query?: Methods_13dmkbk['put']['query'] | undefined, config?: T | undefined }) =>
            fetch<Methods_13dmkbk['put']['resBody'], BasicHeaders, Methods_13dmkbk['put']['status']>(prefix, PATH18, PUT, option, 'FormData').json().then(r => r.body),
          $path: (option?: { method: 'put'; query: Methods_13dmkbk['put']['query'] } | undefined) =>
            `${prefix}${PATH18}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        MSPInsertImageAccessory: {
          post: (option: { body: Methods_1sd1oup['post']['reqBody'], query?: Methods_1sd1oup['post']['query'] | undefined, config?: T | undefined }) =>
            fetch<Methods_1sd1oup['post']['resBody'], BasicHeaders, Methods_1sd1oup['post']['status']>(prefix, PATH19, POST, option, 'FormData').json(),
          $post: (option: { body: Methods_1sd1oup['post']['reqBody'], query?: Methods_1sd1oup['post']['query'] | undefined, config?: T | undefined }) =>
            fetch<Methods_1sd1oup['post']['resBody'], BasicHeaders, Methods_1sd1oup['post']['status']>(prefix, PATH19, POST, option, 'FormData').json().then(r => r.body),
          $path: (option?: { method: 'post'; query: Methods_1sd1oup['post']['query'] } | undefined) =>
            `${prefix}${PATH19}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        Momo: {
          Order: {
            get: (option?: { config?: T | undefined } | undefined) =>
              fetch<Methods_vss6uo['get']['resBody'], BasicHeaders, Methods_vss6uo['get']['status']>(prefix, PATH21, GET, option).blob(),
            $get: (option?: { config?: T | undefined } | undefined) =>
              fetch<Methods_vss6uo['get']['resBody'], BasicHeaders, Methods_vss6uo['get']['status']>(prefix, PATH21, GET, option).blob().then(r => r.body),
            $path: () => `${prefix}${PATH21}`,
          },
          get: (option?: { config?: T | undefined } | undefined) =>
            fetch<Methods_1k8tpuv['get']['resBody'], BasicHeaders, Methods_1k8tpuv['get']['status']>(prefix, PATH20, GET, option).blob(),
          $get: (option?: { config?: T | undefined } | undefined) =>
            fetch<Methods_1k8tpuv['get']['resBody'], BasicHeaders, Methods_1k8tpuv['get']['status']>(prefix, PATH20, GET, option).blob().then(r => r.body),
          $path: () => `${prefix}${PATH20}`,
        },
        OPSAddPlanRefund: {
          post: (option: { body: Methods_f44lv5['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_f44lv5['post']['resBody'], BasicHeaders, Methods_f44lv5['post']['status']>(prefix, PATH22, POST, option).json(),
          $post: (option: { body: Methods_f44lv5['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_f44lv5['post']['resBody'], BasicHeaders, Methods_f44lv5['post']['status']>(prefix, PATH22, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH22}`,
        },
        OPSBuyingPlan: {
          post: (option: { body: Methods_bvea9y['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_bvea9y['post']['resBody'], BasicHeaders, Methods_bvea9y['post']['status']>(prefix, PATH23, POST, option).json(),
          $post: (option: { body: Methods_bvea9y['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_bvea9y['post']['resBody'], BasicHeaders, Methods_bvea9y['post']['status']>(prefix, PATH23, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH23}`,
        },
        RROApproveRefundRequestOrder: {
          patch: (option: { body: Methods_9j6uh0['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_9j6uh0['patch']['resBody'], BasicHeaders, Methods_9j6uh0['patch']['status']>(prefix, PATH24, PATCH, option).json(),
          $patch: (option: { body: Methods_9j6uh0['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_9j6uh0['patch']['resBody'], BasicHeaders, Methods_9j6uh0['patch']['status']>(prefix, PATH24, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH24}`,
        },
        RRODeleteRefundRequestOrder: {
          patch: (option: { body: Methods_vucz9i['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_vucz9i['patch']['resBody'], BasicHeaders, Methods_vucz9i['patch']['status']>(prefix, PATH25, PATCH, option).json(),
          $patch: (option: { body: Methods_vucz9i['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_vucz9i['patch']['resBody'], BasicHeaders, Methods_vucz9i['patch']['status']>(prefix, PATH25, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH25}`,
        },
        RROInsertRefundRequestOrder: {
          post: (option: { body: Methods_1lte7bs['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1lte7bs['post']['resBody'], BasicHeaders, Methods_1lte7bs['post']['status']>(prefix, PATH26, POST, option).json(),
          $post: (option: { body: Methods_1lte7bs['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1lte7bs['post']['resBody'], BasicHeaders, Methods_1lte7bs['post']['status']>(prefix, PATH26, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH26}`,
        },
        RROSelectRefundRequestOrder: {
          get: (option?: { query?: Methods_uzk94p['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_uzk94p['get']['resBody'], BasicHeaders, Methods_uzk94p['get']['status']>(prefix, PATH27, GET, option).json(),
          $get: (option?: { query?: Methods_uzk94p['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_uzk94p['get']['resBody'], BasicHeaders, Methods_uzk94p['get']['status']>(prefix, PATH27, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_uzk94p['get']['query'] } | undefined) =>
            `${prefix}${PATH27}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        RROSelectRefundRequestOrders: {
          get: (option?: { query?: Methods_772ni6['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_772ni6['get']['resBody'], BasicHeaders, Methods_772ni6['get']['status']>(prefix, PATH28, GET, option).json(),
          $get: (option?: { query?: Methods_772ni6['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_772ni6['get']['resBody'], BasicHeaders, Methods_772ni6['get']['status']>(prefix, PATH28, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_772ni6['get']['query'] } | undefined) =>
            `${prefix}${PATH28}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        RROUpdateRefundRequestOrder: {
          patch: (option: { body: Methods_14wigjo['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_14wigjo['patch']['resBody'], BasicHeaders, Methods_14wigjo['patch']['status']>(prefix, PATH29, PATCH, option).json(),
          $patch: (option: { body: Methods_14wigjo['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_14wigjo['patch']['resBody'], BasicHeaders, Methods_14wigjo['patch']['status']>(prefix, PATH29, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH29}`,
        },
        SelectCategories: {
          get: (option?: { query?: Methods_1ywrqxf['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1ywrqxf['get']['resBody'], BasicHeaders, Methods_1ywrqxf['get']['status']>(prefix, PATH30, GET, option).json(),
          $get: (option?: { query?: Methods_1ywrqxf['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1ywrqxf['get']['resBody'], BasicHeaders, Methods_1ywrqxf['get']['status']>(prefix, PATH30, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1ywrqxf['get']['query'] } | undefined) =>
            `${prefix}${PATH30}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        SelectCategory: {
          get: (option?: { query?: Methods_wgnsfn['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_wgnsfn['get']['resBody'], BasicHeaders, Methods_wgnsfn['get']['status']>(prefix, PATH31, GET, option).json(),
          $get: (option?: { query?: Methods_wgnsfn['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_wgnsfn['get']['resBody'], BasicHeaders, Methods_wgnsfn['get']['status']>(prefix, PATH31, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_wgnsfn['get']['query'] } | undefined) =>
            `${prefix}${PATH31}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        SelectSubCategories: {
          get: (option?: { query?: Methods_16r358v['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_16r358v['get']['resBody'], BasicHeaders, Methods_16r358v['get']['status']>(prefix, PATH32, GET, option).json(),
          $get: (option?: { query?: Methods_16r358v['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_16r358v['get']['resBody'], BasicHeaders, Methods_16r358v['get']['status']>(prefix, PATH32, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_16r358v['get']['query'] } | undefined) =>
            `${prefix}${PATH32}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        UDSDeleteUserAddress: {
          patch: (option: { body: Methods_ecpjzp['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_ecpjzp['patch']['resBody'], BasicHeaders, Methods_ecpjzp['patch']['status']>(prefix, PATH33, PATCH, option).json(),
          $patch: (option: { body: Methods_ecpjzp['patch']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_ecpjzp['patch']['resBody'], BasicHeaders, Methods_ecpjzp['patch']['status']>(prefix, PATH33, PATCH, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH33}`,
        },
        UDSInsertUserAddress: {
          post: (option: { body: Methods_1othzpb['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1othzpb['post']['resBody'], BasicHeaders, Methods_1othzpb['post']['status']>(prefix, PATH34, POST, option).json(),
          $post: (option: { body: Methods_1othzpb['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1othzpb['post']['resBody'], BasicHeaders, Methods_1othzpb['post']['status']>(prefix, PATH34, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH34}`,
        },
        UDSSelectUserAddress: {
          get: (option?: { query?: Methods_1i0gbdw['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1i0gbdw['get']['resBody'], BasicHeaders, Methods_1i0gbdw['get']['status']>(prefix, PATH35, GET, option).json(),
          $get: (option?: { query?: Methods_1i0gbdw['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1i0gbdw['get']['resBody'], BasicHeaders, Methods_1i0gbdw['get']['status']>(prefix, PATH35, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1i0gbdw['get']['query'] } | undefined) =>
            `${prefix}${PATH35}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        UDSSelectUserProfile: {
          get: (option?: { query?: Methods_1smrvw1['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1smrvw1['get']['resBody'], BasicHeaders, Methods_1smrvw1['get']['status']>(prefix, PATH36, GET, option).json(),
          $get: (option?: { query?: Methods_1smrvw1['get']['query'] | undefined, config?: T | undefined } | undefined) =>
            fetch<Methods_1smrvw1['get']['resBody'], BasicHeaders, Methods_1smrvw1['get']['status']>(prefix, PATH36, GET, option).json().then(r => r.body),
          $path: (option?: { method?: 'get' | undefined; query: Methods_1smrvw1['get']['query'] } | undefined) =>
            `${prefix}${PATH36}${option && option.query ? `?${dataToURLString(option.query)}` : ''}`,
        },
        UDSUpdateUserAddress: {
          put: (option: { body: Methods_pfkw7b['put']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_pfkw7b['put']['resBody'], BasicHeaders, Methods_pfkw7b['put']['status']>(prefix, PATH37, PUT, option).json(),
          $put: (option: { body: Methods_pfkw7b['put']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_pfkw7b['put']['resBody'], BasicHeaders, Methods_pfkw7b['put']['status']>(prefix, PATH37, PUT, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH37}`,
        },
        UDSUpdateUserProfile: {
          put: (option: { body: Methods_o6khhm['put']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_o6khhm['put']['resBody'], BasicHeaders, Methods_o6khhm['put']['status']>(prefix, PATH38, PUT, option).json(),
          $put: (option: { body: Methods_o6khhm['put']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_o6khhm['put']['resBody'], BasicHeaders, Methods_o6khhm['put']['status']>(prefix, PATH38, PUT, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH38}`,
        },
        URSUserRegister: {
          post: (option: { body: Methods_1bn80az['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1bn80az['post']['resBody'], BasicHeaders, Methods_1bn80az['post']['status']>(prefix, PATH39, POST, option).json(),
          $post: (option: { body: Methods_1bn80az['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_1bn80az['post']['resBody'], BasicHeaders, Methods_1bn80az['post']['status']>(prefix, PATH39, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH39}`,
        },
        UpdateCategory: {
          put: (option: { body: Methods_t8kgc6['put']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_t8kgc6['put']['resBody'], BasicHeaders, Methods_t8kgc6['put']['status']>(prefix, PATH40, PUT, option).json(),
          $put: (option: { body: Methods_t8kgc6['put']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_t8kgc6['put']['resBody'], BasicHeaders, Methods_t8kgc6['put']['status']>(prefix, PATH40, PUT, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH40}`,
        },
        VEXSAddToQueue: {
          post: (option: { body: Methods_7pt71i['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_7pt71i['post']['resBody'], BasicHeaders, Methods_7pt71i['post']['status']>(prefix, PATH41, POST, option).json(),
          $post: (option: { body: Methods_7pt71i['post']['reqBody'], config?: T | undefined }) =>
            fetch<Methods_7pt71i['post']['resBody'], BasicHeaders, Methods_7pt71i['post']['status']>(prefix, PATH41, POST, option).json().then(r => r.body),
          $path: () => `${prefix}${PATH41}`,
        },
      },
    },
  };
};

export type ApiInstance = ReturnType<typeof api>;
export default api;
