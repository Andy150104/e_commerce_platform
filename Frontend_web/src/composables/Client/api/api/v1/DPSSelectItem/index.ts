/* eslint-disable */
import type { DefineMethods } from 'aspida';
import type * as Types from '../../../@types';

export type Methods = DefineMethods<{
  get: {
    query?: {
      SearchBy?: number | undefined;
      SortBy?: number | null | undefined;
      MinimumPrice?: number | null | undefined;
      MaximumPrice?: number | null | undefined;
      NameAccessory?: string | null | undefined;
      ChildCategoryName?: string | null | undefined;
      ParentCategoryName?: string | null | undefined;
      CurrentPage?: number | undefined;
      PageSize?: number | undefined;
    } | undefined;

    status: 200;
    resBody: Types.DPSSelectItemResponse;
  };
}>;
