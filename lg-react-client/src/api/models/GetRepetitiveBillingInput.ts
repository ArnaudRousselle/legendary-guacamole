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

import { mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface GetRepetitiveBillingInput
 */
export interface GetRepetitiveBillingInput {
    /**
     * 
     * @type {string}
     * @memberof GetRepetitiveBillingInput
     */
    id: string;
}

/**
 * Check if a given object implements the GetRepetitiveBillingInput interface.
 */
export function instanceOfGetRepetitiveBillingInput(value: object): value is GetRepetitiveBillingInput {
    if (!('id' in value) || value['id'] === undefined) return false;
    return true;
}

export function GetRepetitiveBillingInputFromJSON(json: any): GetRepetitiveBillingInput {
    return GetRepetitiveBillingInputFromJSONTyped(json, false);
}

export function GetRepetitiveBillingInputFromJSONTyped(json: any, ignoreDiscriminator: boolean): GetRepetitiveBillingInput {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'],
    };
}

export function GetRepetitiveBillingInputToJSON(value?: GetRepetitiveBillingInput | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'id': value['id'],
    };
}

