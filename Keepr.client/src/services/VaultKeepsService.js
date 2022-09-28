import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {
  async createVaultKeep(newVaultKeep) {
    logger.log(newVaultKeep);
    const res = await api.post(`/api/vaultkeeps`, newVaultKeep);
    logger.log("New VaultKeep", res.data);
    AppState.vaultKeeps.push(res.data);
  }

  async setActive(vaultKeep) {
    logger.log("setting active", vaultKeep);
    const foundKeep = await api.get(`api/keeps/${vaultKeep.id}`);
    logger.log(foundKeep.data);
    AppState.activeVaultKeep = vaultKeep;
  }

  async deleteVaultKeep(id) {
    const res = await api.delete(`/api/vaultkeeps/${id}`);
    logger.log("Deleting VaultKeep", res.data);
    Pop.toast(res.data, "warning");
    AppState.vaultKeeps = AppState.vaultKeeps.filter(
      (vk) => vk.vaultKeepId != id
    );
  }
}

export const vaultKeepsService = new VaultKeepsService();
