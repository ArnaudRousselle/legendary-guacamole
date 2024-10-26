// fichier auto-généré !!
import {
  WorkspaceApi,
} from "../api";
import { useApiRef } from "../hooks";

export interface IBaseApiContext {
  workspaceApi: WorkspaceApi;
}

export const defaultBaseApiContextValues: IBaseApiContext = {
  workspaceApi: new WorkspaceApi(),
};

export function useAllApisRef(): IBaseApiContext {
  const workspaceApiRef = useApiRef(WorkspaceApi);

  return {
    workspaceApi: workspaceApiRef.current,
  };
}
