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
 * @interface DeleteBillingInput
 */
export interface DeleteBillingInput {
    /**
     * 
     * @type {string}
     * @memberof DeleteBillingInput
     */
    id: string;
}

/**
 * Check if a given object implements the DeleteBillingInput interface.
 */
export function instanceOfDeleteBillingInput(value: object): value is DeleteBillingInput {
    if (!('id' in value) || value['id'] === undefined) return false;
    return true;
}

export function DeleteBillingInputFromJSON(json: any): DeleteBillingInput {
    return DeleteBillingInputFromJSONTyped(json, false);
}

export function DeleteBillingInputFromJSONTyped(json: any, ignoreDiscriminator: boolean): DeleteBillingInput {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'],
    };
}

export function DeleteBillingInputToJSON(value?: DeleteBillingInput | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'id': value['id'],
    };
}
