<template>
  <div class="area-pad">
    <div class="area-item" v-for="area in areaInfo">
      <p class="title">
        {{ area.areaName }}
        <span
          >分区版主：
          <i
            style="margin-right: 10px"
            v-for="areaLorder in area.areaPadLorders"
            >{{ areaLorder.userName }}</i
          >
        </span>
      </p>
      <div class="section-pad">
        <table>
          <tr v-for="rowIndex in getRowCount(area)">
            <td
              v-for="section in getSectionByRowIndex(rowIndex, area.sections)">
              <router-link :to="`/postList/${section.id}/1`">
                <div>
                  <div class="section-item-img">
                    <img :src="section.sectionIcon" alt="" />
                  </div>
                  <div class="section-item-info">
                    <p class="s-name">{{ section.sectionTitle }}</p>
                    <p class="s-descript">{{ section.sectionDescription }}</p>
                    <p>
                      版主：<router-link
                        to="/aaa"
                        v-for="sectionLorder in section.sectionLorders"
                        >{{ sectionLorder.userName }}</router-link
                      >
                    </p>
                  </div>
                </div>
              </router-link>
            </td>
          </tr>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = defineProps({
  areaInfo: {
    type: Object,
  },
});

const rowSize = 3;
const getRowCount = (area: any) => {
  return Math.ceil(area.sections.length / rowSize);
};

const getSectionByRowIndex = (rowIndex: number, sections: any) => {
  let sectionsByRowIndex = [];
  let start = (rowIndex - 1) * rowSize;
  let end = start + rowSize;
  for (let i = 0; i < sections.length; i++) {
    if (i >= start && i < end) {
      sectionsByRowIndex.push(sections[i]);
    }
  }

  console.log(sectionsByRowIndex[0].id);

  return sectionsByRowIndex;
};
</script>

<style lang="scss" scoped>
.area-pad {
  border: 1px solid #ccc;
  .area-item {
    .title {
      background-color: #eee;
      font-weight: 800;
      font-size: 13px;
      height: 36px;
      line-height: 36px;
      padding-left: 10px;
    }

    .section-pad {
      padding: 0 10px;
      table {
        width: 100%;
        tr {
          display: flex;
          border-top: 1px #ccc dashed;
          &:first-child {
            border-top: none 0;
          }

          td {
            flex: 1;
            padding: 10px 0 10px 10px;
            a {
              text-decoration: none;
              color: #333;
              .section-item-img {
                float: left;
                margin-right: 10px;
                height: 100%;
                img {
                  width: 100px;
                  height: 58px;
                }
              }
              .section-item-info {
                p {
                  font-size: 13px;
                }
                .s-name {
                  font-weight: 800;
                  font-size: 13px;
                }
                .s-descript {
                  padding: 3px 0;
                }
              }
            }
          }
        }
      }
    }
  }
}
</style>
