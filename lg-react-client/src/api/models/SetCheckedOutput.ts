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
 * @interface SetCheckedOutput
 */
export interface SetCheckedOutput {
    /**
     * 
     * @type {string}
     * @memberof SetCheckedOutput
     */
    id: string;
    /**
     * 
     * @type {boolean}
     * @memberof SetCheckedOutput
     */
    checked: boolean;
}

/**
 * Check if a given object implements the SetCheckedOutput interface.
 */
export function instanceOfSetCheckedOutput(value: object): value is SetCheckedOutput {
    if (!('id' in value) || value['id'] === undefined) return false;
    if (!('checked' in value) || value['checked'] === undefined) return false;
    return true;
}

export function SetCheckedOutputFromJSON(json: any): SetCheckedOutput {
    return SetCheckedOutputFromJSONTyped(json, false);
}

export function SetCheckedOutputFromJSONTyped(json: any, ignoreDiscriminator: boolean): SetCheckedOutput {
    if (json == null) {
        return json;
    }
    return {
        
        'id': json['id'],
        'checked': json['checked'],
    };
}

export function SetCheckedOutputToJSON(value?: SetCheckedOutput | null): any {
    if (value == null) {
        return value;
    }
    return {
        
        'id': value['id'],
        'checked': value['checked'],
    };
}
