/* eslint-disable */
import fs from "fs";
import pkg from "glob";

const { sync: globSync } = pkg;
const { writeFileSync } = fs;

const APIS_PATH = "./src/api/apis/*.ts";
const OUTPUT_FILE = "./src/contexts/apiUtils.ts";

const classNames = globSync(APIS_PATH)
  .map((path) => {
    const args = path.split("/");
    const fileName = args[args.length - 1];
    const className = fileName.substring(0, fileName.indexOf("."));
    return className;
  })
  .filter((n) => n !== "index");

if (classNames.length === 0) process.exit(1);

writeFileSync(
  OUTPUT_FILE,
  `// fichier auto-généré !!
import {
${classNames.map((c) => `  ${c},`).join("\n")}
} from "../api";
import { useApiRef } from "../hooks";

export interface IBaseApiContext {
${classNames.map((c) => `  ${camelCase(c)}: ${c};`).join("\n")}
}

export const defaultBaseApiContextValues: IBaseApiContext = {
${classNames.map((c) => `  ${camelCase(c)}: new ${c}(),`).join("\n")}
};

export function useAllApisRef(): IBaseApiContext {
${classNames
  .map((c) => `  const ${camelCase(c)}Ref = useApiRef(${c});`)
  .join("\n")}

  return {
${classNames
  .map((c) => `    ${camelCase(c)}: ${camelCase(c)}Ref.current,`)
  .join("\n")}
  };
}
`
);

console.log("ok!");
process.exit(0);

function camelCase(str) {
  return str.substr(0, 1).toLowerCase() + str.substr(1);
}
