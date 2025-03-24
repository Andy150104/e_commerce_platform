/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  get: {
    query?: {
      ExchangeId?: string | undefined;
    } | undefined;

    status: 200;
    resBody: Types.AEPSGetByIdExchangeAccessoryResponse;
  };
}>;
