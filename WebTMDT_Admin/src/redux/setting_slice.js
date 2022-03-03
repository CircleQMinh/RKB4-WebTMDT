import { createSlice } from '@reduxjs/toolkit';

const setting_slice = createSlice({
  name: 'setting_slice',
  initialState: { apiUrl: "https://localhost:7251/api" },
  reducers: {
    toggle(state,action) {
      state.apiUrl = action.payload;
    }
  }
});

export const setting_slice_action = setting_slice.actions;

export default setting_slice;