import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async getVaultById(id) {
    const res = await api.get(`/api/vaults/${id}`);
    logger.log("Getting vault by Id", res.data);
    AppState.activeVault = res.data;
  }

  async create(vault) {
    const res = await api.post(`/api/vaults`, vault);
    logger.log("Created vault", res.data);
    AppState.profileVaults.push(res.data);
  }

  async edit(vault) {
    const res = await api.put(`/api/vaults/` + vault.id, vault);
    logger.log("Editing Vault", res.data);
    AppState.activeVault = res.data;
  }

  // async deleteVault(id) {
  //   const res = await api.delete(`/api/vaults/` + id);
  //   logger.log("Deleting vault", res.data);
  //   AppState.profileVaults = AppState.profileVaults.filter((v) => v.id != id);
  // }
}

export const vaultsService = new VaultsService();
