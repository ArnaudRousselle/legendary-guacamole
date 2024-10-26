/* tslint:disable */
/* eslint-disable */
/**
 * LegendaryGuacamole.WebApi
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  AddBillingInput,
  AddBillingOutput,
  DeleteBillingInput,
  DeleteBillingOutput,
  EditBillingInput,
  EditBillingOutput,
  GetBillingInput,
  GetBillingOutput,
  GetSummaryResult,
  ListBillingsInput,
  ListBillingsOutput,
  SetCheckedInput,
  SetCheckedOutput,
} from '../models/index';
import {
    AddBillingInputFromJSON,
    AddBillingInputToJSON,
    AddBillingOutputFromJSON,
    AddBillingOutputToJSON,
    DeleteBillingInputFromJSON,
    DeleteBillingInputToJSON,
    DeleteBillingOutputFromJSON,
    DeleteBillingOutputToJSON,
    EditBillingInputFromJSON,
    EditBillingInputToJSON,
    EditBillingOutputFromJSON,
    EditBillingOutputToJSON,
    GetBillingInputFromJSON,
    GetBillingInputToJSON,
    GetBillingOutputFromJSON,
    GetBillingOutputToJSON,
    GetSummaryResultFromJSON,
    GetSummaryResultToJSON,
    ListBillingsInputFromJSON,
    ListBillingsInputToJSON,
    ListBillingsOutputFromJSON,
    ListBillingsOutputToJSON,
    SetCheckedInputFromJSON,
    SetCheckedInputToJSON,
    SetCheckedOutputFromJSON,
    SetCheckedOutputToJSON,
} from '../models/index';

export interface AddBillingQueryRequest {
    addBillingInput: AddBillingInput;
}

export interface DeleteBillingQueryRequest {
    deleteBillingInput: DeleteBillingInput;
}

export interface EditBillingQueryRequest {
    editBillingInput: EditBillingInput;
}

export interface GetBillingQueryRequest {
    getBillingInput: GetBillingInput;
}

export interface GetSummaryQueryRequest {
    body: object;
}

export interface ListBillingsQueryRequest {
    listBillingsInput: ListBillingsInput;
}

export interface SetCheckedQueryRequest {
    setCheckedInput: SetCheckedInput;
}

/**
 * 
 */
export class WorkspaceApi extends runtime.BaseAPI {

    /**
     */
    async addBillingQueryRaw(requestParameters: AddBillingQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<AddBillingOutput>> {
        if (requestParameters['addBillingInput'] == null) {
            throw new runtime.RequiredError(
                'addBillingInput',
                'Required parameter "addBillingInput" was null or undefined when calling addBillingQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/addBillingQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: AddBillingInputToJSON(requestParameters['addBillingInput']),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => AddBillingOutputFromJSON(jsonValue));
    }

    /**
     */
    async addBillingQuery(addBillingInput: AddBillingInput, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<AddBillingOutput> {
        const response = await this.addBillingQueryRaw({ addBillingInput: addBillingInput }, initOverrides);
        return await response.value();
    }

    /**
     */
    async deleteBillingQueryRaw(requestParameters: DeleteBillingQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<DeleteBillingOutput>> {
        if (requestParameters['deleteBillingInput'] == null) {
            throw new runtime.RequiredError(
                'deleteBillingInput',
                'Required parameter "deleteBillingInput" was null or undefined when calling deleteBillingQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/deleteBillingQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: DeleteBillingInputToJSON(requestParameters['deleteBillingInput']),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => DeleteBillingOutputFromJSON(jsonValue));
    }

    /**
     */
    async deleteBillingQuery(deleteBillingInput: DeleteBillingInput, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<DeleteBillingOutput> {
        const response = await this.deleteBillingQueryRaw({ deleteBillingInput: deleteBillingInput }, initOverrides);
        return await response.value();
    }

    /**
     */
    async editBillingQueryRaw(requestParameters: EditBillingQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<EditBillingOutput>> {
        if (requestParameters['editBillingInput'] == null) {
            throw new runtime.RequiredError(
                'editBillingInput',
                'Required parameter "editBillingInput" was null or undefined when calling editBillingQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/editBillingQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: EditBillingInputToJSON(requestParameters['editBillingInput']),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => EditBillingOutputFromJSON(jsonValue));
    }

    /**
     */
    async editBillingQuery(editBillingInput: EditBillingInput, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<EditBillingOutput> {
        const response = await this.editBillingQueryRaw({ editBillingInput: editBillingInput }, initOverrides);
        return await response.value();
    }

    /**
     */
    async getBillingQueryRaw(requestParameters: GetBillingQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GetBillingOutput>> {
        if (requestParameters['getBillingInput'] == null) {
            throw new runtime.RequiredError(
                'getBillingInput',
                'Required parameter "getBillingInput" was null or undefined when calling getBillingQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/getBillingQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: GetBillingInputToJSON(requestParameters['getBillingInput']),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GetBillingOutputFromJSON(jsonValue));
    }

    /**
     */
    async getBillingQuery(getBillingInput: GetBillingInput, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GetBillingOutput> {
        const response = await this.getBillingQueryRaw({ getBillingInput: getBillingInput }, initOverrides);
        return await response.value();
    }

    /**
     */
    async getSummaryQueryRaw(requestParameters: GetSummaryQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<GetSummaryResult>> {
        if (requestParameters['body'] == null) {
            throw new runtime.RequiredError(
                'body',
                'Required parameter "body" was null or undefined when calling getSummaryQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/getSummaryQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: requestParameters['body'] as any,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => GetSummaryResultFromJSON(jsonValue));
    }

    /**
     */
    async getSummaryQuery(body: object, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<GetSummaryResult> {
        const response = await this.getSummaryQueryRaw({ body: body }, initOverrides);
        return await response.value();
    }

    /**
     */
    async listBillingsQueryRaw(requestParameters: ListBillingsQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<ListBillingsOutput>>> {
        if (requestParameters['listBillingsInput'] == null) {
            throw new runtime.RequiredError(
                'listBillingsInput',
                'Required parameter "listBillingsInput" was null or undefined when calling listBillingsQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/listBillingsQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: ListBillingsInputToJSON(requestParameters['listBillingsInput']),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(ListBillingsOutputFromJSON));
    }

    /**
     */
    async listBillingsQuery(listBillingsInput: ListBillingsInput, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<ListBillingsOutput>> {
        const response = await this.listBillingsQueryRaw({ listBillingsInput: listBillingsInput }, initOverrides);
        return await response.value();
    }

    /**
     */
    async setCheckedQueryRaw(requestParameters: SetCheckedQueryRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<SetCheckedOutput>> {
        if (requestParameters['setCheckedInput'] == null) {
            throw new runtime.RequiredError(
                'setCheckedInput',
                'Required parameter "setCheckedInput" was null or undefined when calling setCheckedQuery().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/setCheckedQuery`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: SetCheckedInputToJSON(requestParameters['setCheckedInput']),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => SetCheckedOutputFromJSON(jsonValue));
    }

    /**
     */
    async setCheckedQuery(setCheckedInput: SetCheckedInput, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<SetCheckedOutput> {
        const response = await this.setCheckedQueryRaw({ setCheckedInput: setCheckedInput }, initOverrides);
        return await response.value();
    }

}